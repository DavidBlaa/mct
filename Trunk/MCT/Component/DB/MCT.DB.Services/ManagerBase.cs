using NHibernate;
using NHibernate.Linq;
using System.Linq;

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

        public IQueryable<T> GetAllAsQueryable<T>()
        {
            return CurrentNHibernateSession.Query<T>();
        }

        public T Get(V v)
        {
            return (T)CurrentNHibernateSession.Load(typeof(T), v);
        }

        public T Create(T t)
        {
            using (ITransaction transaction = CurrentNHibernateSession.BeginTransaction())
            {
                CurrentNHibernateSession.Save(t);
                transaction.Commit();

                return t;
            }
        }

        public void Update(T t)
        {
            using (ITransaction transaction = CurrentNHibernateSession.BeginTransaction())
            {
                CurrentNHibernateSession.SaveOrUpdate(t);
                transaction.Commit();
            }
        }

        public void Delete(T t)
        {
            using (ITransaction transaction = CurrentNHibernateSession.BeginTransaction())
            {
                CurrentNHibernateSession.Delete(t);
                transaction.Commit();
            }
        }
    }
}
