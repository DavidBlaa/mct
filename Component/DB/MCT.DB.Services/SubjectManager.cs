using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCT.DB.Services
{
    public class SubjectManager : ManagerBase<Subject, long>
    {

        public SubjectManager()
        {
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

        #region Create

        public Plant CreatePlant(Plant plant)
        {
            plant = Create(plant);

            // go to each timerperiod and create
            #region go to each timerperiod and create

            foreach (var tp in plant.TimePeriods)
            {
                if (tp.Id == 0)
                {
                    //tp.AssignedTo = plant;
                    var newtp = Create(tp);
                    tp.Id = newtp.Id;
                }
            }

            Update(plant);
            #endregion

            return plant;
        }

        public Plant UpdatePlant(Plant plant)
        {

            foreach (var tp in plant.TimePeriods)
            {
                if (tp.Id == 0)
                {
                    tp.AssignedTo = plant;
                    var newtp = Create(tp);
                    tp.Id = newtp.Id;
                }
            }


            this.Update(plant);

            return plant;
        }

        public Animal CreateAnimal(Animal animal)
        {
            Animal tmp = Create(animal);

            // go to each timerperiod and create
            #region go to each timerperiod and create

            foreach (var tp in animal.TimePeriods)
            {
                if (tp.Id == 0)
                {
                    tp.AssignedTo = animal;
                    var newtp = Create(tp);
                    tp.Id = newtp.Id;
                }
            }

            #endregion

            return tmp;
        }

        public Taxon CreateTaxon(Taxon taxon)
        {
            Taxon tmp = Create(taxon);
            return tmp;
        }

        public void DeleteNode(Node node)
        {
            try
            {
                List<Interaction> interactons = GetAllDependingInteractions(node).ToList();
                InteractionManager interactionManager = new InteractionManager();

                for (int i = 0; i < interactons.Count; i++)
                {
                    interactionManager.Delete(interactons[i]);
                }

                List<Node> children = this.GetAll<Node>().Where(n => n.Parent != null && n.Parent.Id.Equals(node.Id)).ToList();

                foreach (Node child in children)
                {
                    child.Parent = null;
                    Update(child);
                }


                Delete(node);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

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
        public IEnumerable<Interaction> GetAllDependingInteractions(Node species, bool recursive = false)
        {
            //var interactions = from x in GetAll<Interaction>()
            //                   where subject != null && (x.Subject.Name.Equals(subject.Name)
            //                            || x.Object.Name.Equals(subject.Name)
            //                            || (x.ImpactSubject == null || x.ImpactSubject.Name.Equals(subject.Name)))
            //                   select x;

            List<Interaction> interactions = new List<Interaction>();


            //get parents
            if (species.Parent != null && recursive)
            {
                interactions.AddRange(GetAllDependingInteractions(species.Parent, recursive));
            }

            foreach (var interaction in GetAll<Interaction>())
            {
                if (interaction.Subject.Name.Equals(species.Name)
                    || interaction.Object.Name.Equals(species.Name)
                    || (interaction.ImpactSubject != null && interaction.ImpactSubject.Name.Equals(species.Name)))
                {
                    interactions.Add(interaction);
                }
            }



            return interactions;
        }
    }
}
