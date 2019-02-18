using MCT.DB.Entities;
using MCT.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Services
{
    public class PredicateManager : ManagerBase<Predicate, long>
    {
        public PredicateManager()
        {
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

        public IEnumerable<Predicate> GetAll()
        {
            return GetAll<Predicate>();
        }
    }
}