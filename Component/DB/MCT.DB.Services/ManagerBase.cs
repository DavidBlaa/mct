using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace MCT.DB.Services
{
    public abstract class ManagerBase<T, V> : IManager<T, V>
                                                where T : new()
                                                where V : struct
    {
        protected ISession CurrentNHibernateSession;

        public T Create<T>(T t)
        {
            using (ITransaction transaction = CurrentNHibernateSession.BeginTransaction())
            {
                CurrentNHibernateSession.Save(t);
                transaction.Commit();
            }

            return t;
        }

        public T[] GetAll<T>()
        {
            var icriteria = CurrentNHibernateSession.Query<T>().ToList();
            return icriteria.ToArray();
        }

        public T Get(V v)
        {
            return (T) CurrentNHibernateSession.Load(typeof(T), v);
        }

        public void Update(T t)
        {
            using (ITransaction transaction = CurrentNHibernateSession.BeginTransaction())
            {
                CurrentNHibernateSession.SaveOrUpdate(t);
                transaction.Commit();
            }
        }
    }
}
