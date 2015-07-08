using System.Collections.Generic;
using System.Linq;
using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate;
using NHibernate.Criterion;

namespace MCT.DB.Services
{
    public class SubjectManager:ManagerBase<Subject, long>
    {

        public SubjectManager()
        {
            
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

        //Example zum get einträge von aus einer spalte als liste
        public List<string> GetAllNames()
        {
            ICriteria stateSearchCriteria = CurrentNHibernateSession.CreateCriteria(typeof(Subject));
            stateSearchCriteria.SetProjection(Projections.Distinct(Projections.Property("Name")));

            var list = stateSearchCriteria.List();

            return list.Cast<string>().ToList();
        }

        public List<string> GetAllScientificNames()
        {
            ICriteria stateSearchCriteria = CurrentNHibernateSession.CreateCriteria(typeof(Species));
            stateSearchCriteria.SetProjection(Projections.Distinct(Projections.Property("ScientificName")));

            var list = stateSearchCriteria.List();

            return list.Cast<string>().ToList();
        }
    }
}
