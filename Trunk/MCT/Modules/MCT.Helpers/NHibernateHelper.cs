using System.Web;
using MCT.DB.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Search;
using NHibernate.Search.Impl;

namespace MCT.Helpers
{
    public sealed class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory sessionFactory;

        static NHibernateHelper()
        {

            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            
        }

        public static ISession GetCurrentSession()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = context.Items[CurrentSessionKey] as ISession;

            if (currentSession == null)
            {
                currentSession = sessionFactory.OpenSession();
                context.Items[CurrentSessionKey] = currentSession;
            }

            return currentSession;
        }

        public static void CloseSession()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = context.Items[CurrentSessionKey] as ISession;

            if (currentSession == null)
            {
                // No current session
                return;
            }

            currentSession.Close();
            context.Items.Remove(CurrentSessionKey);
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }

        public static void ReIndex()
        {
            IFullTextSession session = new FullTextSessionImpl(GetCurrentSession());
            ITransaction tx = session.BeginTransaction();
            session.PurgeAll(typeof(Species));
            tx.Commit();

            tx = session.BeginTransaction();

            foreach (object entity in session.CreateCriteria(typeof(Species)).List())
            {
                session.Index(entity);
            }

            tx.Commit();
            session.Close();

        }
    }
}
