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

        /// <summary>
        /// load all depening interactions for a selected subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public IEnumerable<Interaction> GetAllDependingInteractions(Subject subject)
        {
            //var interactions = from x in GetAll<Interaction>()
            //                   where subject != null && (x.Subject.Name.Equals(subject.Name)
            //                            || x.Object.Name.Equals(subject.Name)
            //                            || (x.ImpactSubject == null || x.ImpactSubject.Name.Equals(subject.Name)))
            //                   select x;

            List<Interaction> interactions = new List<Interaction> ();

            foreach (var interaction in GetAll<Interaction>())
            {
                if (interaction.Subject.Name.Equals(subject.Name)
                    || interaction.Object.Name.Equals(subject.Name)
                    || (interaction.ImpactSubject!=null && interaction.ImpactSubject.Name.Equals(subject.Name)))
                {
                    interactions.Add(interaction);
                }
            }

            return interactions;
        }
    }
}
