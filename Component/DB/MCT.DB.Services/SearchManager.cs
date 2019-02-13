using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate.Linq;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCT.DB.Services
{
    public class SearchManager : ManagerBase<Subject, long>
    {
        private const long POSITIV_PREDICATE_ROOT_ID = 1;
        private const long NEGAVTIV_PREDICATE_ROOT_ID = 2;

        public SearchManager()
        {
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

        #region Search

        public IQueryable<Node> Search(Dictionary<string, string> searchCriteria)
        {
            var session = NHibernateHelper.GetCurrentSession();

            IQueryable<Node> speciesQuery = null;
            IQueryable<Plant> plantQuery = null;

            foreach (KeyValuePair<string, string> kvp in searchCriteria)
            {
                switch (kvp.Key)
                {
                    #region free text

                    case "FREETEXT_SEARCH_KEY":
                        {
                            List<long> speciesIdsForMatchingTaxas = GetSpeciesWhereParentMatchs(kvp.Value);

                            if (speciesQuery == null)
                            {
                                if (string.IsNullOrEmpty(kvp.Value))
                                {
                                    speciesQuery = session.Query<Node>();
                                }
                                else
                                {
                                    speciesQuery = session.Query<Node>().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.Description.ToLower()
                                                                          .Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.ScientificName.ToLower()
                                                                          .Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      speciesIdsForMatchingTaxas.Contains(s.Id));
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(kvp.Value))
                                {
                                    speciesQuery = session.Query<Node>();
                                }
                                else
                                {
                                    speciesQuery = speciesQuery.AsQueryable().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.Description.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.ScientificName.ToLower()
                                                                       .Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   speciesIdsForMatchingTaxas.Contains(s.Id));
                                }
                            }

                            break;
                        }

                    #endregion free text

                    #region plants

                    #region Time

                    case "Sowing":
                        {
                            var sowings = getMatchingTimePeriods(GetAll<Sowing>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => sowings.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => sowings.Contains(s)));

                            break;
                        }

                    case "Harvest":
                        {
                            var harvest = getMatchingTimePeriods(GetAll<Harvest>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => harvest.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => harvest.Contains(s)));

                            break;
                        }

                    case "Bloom":
                        {
                            var bloom = getMatchingTimePeriods(GetAll<Bloom>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => bloom.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => bloom.Contains(s)));

                            break;
                        }

                    case "SeedMaturity":
                        {
                            var seedMaturity = getMatchingTimePeriods(GetAll<SeedMaturity>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => seedMaturity.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => seedMaturity.Contains(s)));

                            break;
                        }

                    case "Cultivate":
                        {
                            var cultivates = getMatchingTimePeriods(GetAll<Cultivate>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => cultivates.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => cultivates.Contains(s)));

                            break;
                        }
                    case "Implant":
                        {
                            var implants = getMatchingTimePeriods(GetAll<Implant>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => implants.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => implants.Contains(s)));

                            break;
                        }

                    #endregion Time

                    #region Properties

                    case "NutrientClaim":
                        {
                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => ((int)p.NutrientClaim).Equals(Convert.ToInt32(kvp.Value)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => ((int)p.NutrientClaim).Equals(Convert.ToInt32(kvp.Value)));

                            break;
                        }

                    case "RootDepth":
                        {
                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => ((int)p.RootDepth).Equals(Convert.ToInt32(kvp.Value)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => ((int)p.RootDepth).Equals(Convert.ToInt32(kvp.Value)));

                            break;
                        }

                    #endregion Properties

                    #endregion plants

                    #region interactions

                    case "PositivInteractionOn":
                        {
                            if (speciesQuery == null)
                                speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID);
                            else
                                speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID, speciesQuery);

                            break;
                        }

                    case "NegativInteractionOn":
                        {
                            if (speciesQuery == null)
                                speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID);
                            else
                                speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID, speciesQuery);

                            break;
                        }

                    case "DoPositivInteraction":
                        {
                            if (speciesQuery == null)
                                speciesQuery = getObjectsOfInteractionWithSubject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID);
                            else
                                speciesQuery = getObjectsOfInteractionWithSubject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID, speciesQuery);

                            break;
                        }

                    case "DoNegativInteraction":
                        {
                            if (speciesQuery == null)
                                speciesQuery = getObjectsOfInteractionWithSubject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID);
                            else
                                speciesQuery = getObjectsOfInteractionWithSubject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID, speciesQuery);

                            break;
                        }

                        #endregion interactions
                }
            }

            if (speciesQuery != null && plantQuery != null)
            {
                return (speciesQuery.ToList().Intersect(plantQuery.ToList())).AsQueryable();
            }

            if (speciesQuery != null)
                return speciesQuery;

            if (plantQuery != null)
                return plantQuery;

            return session.Query<Species>();
        }

        public IQueryable<Plant> SearchPlants(Dictionary<string, string> searchCriteria)
        {
            var session = NHibernateHelper.GetCurrentSession();

            IQueryable<Plant> speciesQuery = null;
            IQueryable<Plant> plantQuery = null;

            foreach (KeyValuePair<string, string> kvp in searchCriteria)
            {
                switch (kvp.Key)
                {
                    #region free text

                    case "FREETEXT_SEARCH_KEY":
                        {
                            List<long> speciesIdsForMatchingTaxas = GetSpeciesWhereParentMatchs(kvp.Value);

                            if (speciesQuery == null)
                            {
                                if (string.IsNullOrEmpty(kvp.Value))
                                {
                                    speciesQuery = session.Query<Plant>();
                                }
                                else
                                {
                                    speciesQuery = session.Query<Plant>().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.Description.ToLower()
                                                                          .Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.ScientificName.ToLower()
                                                                          .Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      speciesIdsForMatchingTaxas.Contains(s.Id));
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(kvp.Value))
                                {
                                    speciesQuery = session.Query<Plant>();
                                }
                                else
                                {
                                    speciesQuery = speciesQuery.AsQueryable().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.Description.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.ScientificName.ToLower()
                                                                       .Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   speciesIdsForMatchingTaxas.Contains(s.Id));
                                }
                            }

                            break;
                        }

                    #endregion free text

                    #region plants

                    #region Time

                    case "Sowing":
                        {
                            var sowings = getMatchingTimePeriods(GetAll<Sowing>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => sowings.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => sowings.Contains(s)));

                            break;
                        }

                    case "Harvest":
                        {
                            var harvest = getMatchingTimePeriods(GetAll<Harvest>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => harvest.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => harvest.Contains(s)));

                            break;
                        }

                    case "Bloom":
                        {
                            var bloom = getMatchingTimePeriods(GetAll<Bloom>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => bloom.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => bloom.Contains(s)));

                            break;
                        }

                    case "SeedMaturity":
                        {
                            var seedMaturity = getMatchingTimePeriods(GetAll<SeedMaturity>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => seedMaturity.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => seedMaturity.Contains(s)));

                            break;
                        }

                    case "Cultivate":
                        {
                            var cultivates = getMatchingTimePeriods(GetAll<Cultivate>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => cultivates.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => cultivates.Contains(s)));

                            break;
                        }
                    case "Implant":
                        {
                            var implants = getMatchingTimePeriods(GetAll<Implant>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.TimePeriods.Any(s => implants.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.TimePeriods.Any(s => implants.Contains(s)));

                            break;
                        }

                    #endregion Time

                    #region Properties

                    case "NutrientClaim":
                        {
                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => ((int)p.NutrientClaim).Equals(Convert.ToInt32(kvp.Value)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => ((int)p.NutrientClaim).Equals(Convert.ToInt32(kvp.Value)));

                            break;
                        }

                    case "RootDepth":
                        {
                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => ((int)p.RootDepth).Equals(Convert.ToInt32(kvp.Value)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => ((int)p.RootDepth).Equals(Convert.ToInt32(kvp.Value)));

                            break;
                        }

                        #endregion Properties

                        #endregion plants

                        #region interactions

                        //case "PositivInteractionOn":
                        //    {
                        //        if (speciesQuery == null)
                        //            speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID);
                        //        else
                        //            speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID, speciesQuery);

                        //        break;
                        //    }

                        //case "NegativInteractionOn":
                        //    {
                        //        if (speciesQuery == null)
                        //            speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID);
                        //        else
                        //            speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID, speciesQuery);

                        //        break;
                        //    }

                        //case "DoPositivInteraction":
                        //    {
                        //        if (speciesQuery == null)
                        //            speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID);
                        //        else
                        //            speciesQuery = getSubjectsOfInteractionWithObject(Convert.ToInt64(kvp.Value), POSITIV_PREDICATE_ROOT_ID, speciesQuery);

                        //        break;
                        //    }

                        //case "DoNegativInteraction":
                        //    {
                        //        if (speciesQuery == null)
                        //            speciesQuery = getObjectsOfInteractionWithSubject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID);
                        //        else
                        //            speciesQuery = getObjectsOfInteractionWithSubject(Convert.ToInt64(kvp.Value), NEGAVTIV_PREDICATE_ROOT_ID, speciesQuery);

                        //        break;
                        //    }

                        #endregion interactions
                }
            }

            if (speciesQuery != null && plantQuery != null)
            {
                return (speciesQuery.ToList().Intersect(plantQuery.ToList())).AsQueryable();
            }

            if (speciesQuery != null)
                return speciesQuery;

            if (plantQuery != null)
                return plantQuery;

            return session.Query<Plant>();
        }

        private IEnumerable<TimePeriod> getMatchingTimePeriods(IEnumerable<TimePeriod> tps, TimePeriodMonth month)
        {
            List<TimePeriod> tempTimePeriods = new List<TimePeriod>();

            foreach (TimePeriod VARIABLE in tps)
            {
                if (VARIABLE.StartMonth <= month && VARIABLE.EndMonth >= month)
                    tempTimePeriods.Add(VARIABLE);
            }

            return tempTimePeriods;
        }

        private IQueryable<Species> getSubjectsOfInteractionWithObject(long objectId, long predicateId)
        {
            Predicate rootPredicate =
                this.GetAll<Predicate>().Where(i => i.Id.Equals(predicateId)).FirstOrDefault();

            var matchingInteractionSubjectsIds =
                this.GetAll<Interaction>()
                    .Where(i => i.Predicate.Parent.Equals(rootPredicate) && i.Object.Id.Equals(objectId))
                    .Select(i => i.Subject.Id);

            return this.GetAllAsQueryable<Species>().Where(s => matchingInteractionSubjectsIds.Contains(s.Id));
        }

        private IQueryable<Node> getSubjectsOfInteractionWithObject(long objectId, long predicateId, IQueryable<Node> query)
        {
            Predicate rootPredicate = this.GetAll<Predicate>().FirstOrDefault(i => i.Id.Equals(predicateId));

            var matchingInteractionSubjectsIds = GetAll<Interaction>()
                    .Where(i => i.Predicate.Parent.Equals(rootPredicate) && i.Object.Id.Equals(objectId))
                    .Select(i => i.Subject.Id);

            return query.Where(s => matchingInteractionSubjectsIds.Contains(s.Id));
        }

        private IQueryable<Node> getObjectsOfInteractionWithSubject(long subjectId, long predicateId)
        {
            Predicate rootPredicate =
                this.GetAll<Predicate>().Where(i => i.Id.Equals(predicateId)).FirstOrDefault();

            var matchingInteractionSubjectsIds =
                this.GetAll<Interaction>()
                    .Where(i => i.Predicate.Parent.Equals(rootPredicate) && i.Subject.Id.Equals(subjectId))
                    .Select(i => i.Object.Id);

            return this.GetAllAsQueryable<Node>().Where(s => matchingInteractionSubjectsIds.Contains(s.Id));
        }

        private IQueryable<Node> getObjectsOfInteractionWithSubject(long subjectId, long predicateId, IQueryable<Node> query)
        {
            Predicate rootPredicate = this.GetAll<Predicate>().FirstOrDefault(i => i.Id.Equals(predicateId));

            var matchingInteractionSubjectsIds = GetAll<Interaction>()
                    .Where(i => i.Predicate.Parent.Equals(rootPredicate) && i.Subject.Id.Equals(subjectId))
                    .Select(i => i.Object.Id);

            return query.Where(s => matchingInteractionSubjectsIds.Contains(s.Id));
        }

        private List<long> GetSpeciesWhereParentMatchs(string value)
        {
            List<long> speciesIds = new List<long>();

            SubjectManager _sm = new SubjectManager();

            var species = _sm.GetAll<Node>();

            foreach (var s in species)
            {
                if (IsInTaxonParent(value, s)) speciesIds.Add(s.Id);
            }

            return speciesIds;
        }

        private bool IsInTaxonParent(string value, Node node)
        {
            if (node.Parent != null)
            {
                var parent = node.Parent;
                if (parent.Name.Contains(value) || parent.ScientificName.Contains(value))
                    return true;

                return IsInTaxonParent(value, node.Parent);
            }

            return false;
        }

        #endregion Search
    }
}