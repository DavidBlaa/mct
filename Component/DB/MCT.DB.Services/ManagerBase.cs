using System.Linq;
using NHibernate;

namespace MCT.DB.Services
{
    public abstract class ManagerBase<T, V> : IManager<T, V>
                                                where T : new()
                                                where V : struct
    {
        protected ISession CurrentNHibernateSession;

        public T Create<T>(T t)
        {
            using (ITransaction transaction = this.CurrentNHibernateSession.BeginTransaction())
            {
                this.CurrentNHibernateSession.Save(t);
                transaction.Commit();
            }

            return t;
        }

        public T[] GetAll<T>()
        {
            var icriteria = this.CurrentNHibernateSession.CreateCriteria(typeof(T));
            return icriteria.List<T>().ToArray();
        }

        public T Get(V v)
        {
            return (T) this.CurrentNHibernateSession.Load(typeof(T), v);
        }

        public void Update(T t)
        {
            using (ITransaction transaction = this.CurrentNHibernateSession.BeginTransaction())
            {
                this.CurrentNHibernateSession.SaveOrUpdate(t);
                transaction.Commit();
            }
        }
    }
}
