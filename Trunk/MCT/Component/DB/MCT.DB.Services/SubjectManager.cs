using MCT.DB.Entities;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Services
{
    public class SubjectManager:ManagerBase<Subject, long>
    {

        public SubjectManager(ISession session)
        {
            base.CurrentNHibernateSession = session;
        }

        //Example zum get einträge von aus einer spalte als liste
        public List<string> GetAllNames()
        {
            ICriteria stateSearchCriteria = base.CurrentNHibernateSession.CreateCriteria(typeof(Subject));
            stateSearchCriteria.SetProjection(Projections.Distinct(Projections.Property("Name")));

            var list = stateSearchCriteria.List();

            return list.Cast<string>().ToList();
        }
    }
}
