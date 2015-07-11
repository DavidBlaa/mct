using System.Web;
using MCT.DB.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Search;
using NHibernate.Search.Impl;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System.Diagnostics;

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
            session.PurgeAll(typeof(Subject));
            tx.Commit();

            tx = session.BeginTransaction();

            

            foreach (object entity in session.CreateCriteria(typeof(Subject)).List())
            {
                session.Index(entity);
            }

            tx.Commit();
            session.Close();

        }

        public static void Search()
        {
                HttpContext context = HttpContext.Current;
                ISession currentSession = context.Items[CurrentSessionKey] as ISession;

                if (currentSession == null)
                {
                    currentSession = sessionFactory.OpenSession();
                    context.Items[CurrentSessionKey] = currentSession;
                }

                IFullTextSession session = new FullTextSessionImpl(currentSession);
                if (!currentSession.IsOpen)
                {
                    currentSession = sessionFactory.OpenSession();
                    context.Items[CurrentSessionKey] = currentSession;

                    if (currentSession.IsOpen)
                        Debug.WriteLine("Session open");
                    else
                        Debug.WriteLine("Session Closed");
                }

                string searchQuery = "Name:2";
                Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

                QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Name", std);
                Query query = parser.Parse(searchQuery);

                IFullTextSession fullTextSession = NHibernate.Search.Search.CreateFullTextSession(NHibernateHelper.GetCurrentSession());
                IFullTextQuery fullTextQuery = fullTextSession.CreateFullTextQuery(query, typeof(Species));

                var employeeList = fullTextQuery.List();

        }
    }
}
