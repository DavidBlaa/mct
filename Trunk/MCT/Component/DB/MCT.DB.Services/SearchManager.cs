using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Mapping;
using NHibernate.Util;

namespace MCT.DB.Services
{
    public class SearchManager : ManagerBase<Subject, long>
    {
        public SearchManager()
        {
            
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

        #region Search

        public IQueryable<Species> Search(Dictionary<string,string> searchCriteria )
        {
            var session = NHibernateHelper.GetCurrentSession();

            IQueryable<Species> freeTextQuery = null;
            IQueryable<Plant> plantQuery = null;

            foreach (KeyValuePair<string, string> kvp in searchCriteria)
            {
                switch (kvp.Key)
                {
                    #region free text
                    
                    case "FREETEXT_SEARCH_KEY":
                    {
                        

                        if (freeTextQuery == null)
                        {
                            freeTextQuery = session.Query<Species>().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.Description.ToLower()
                                                                          .Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.ScientificName.ToLower()
                                                                          .Contains(kvp.Value.ToLower()));
                        }
                        else
                        {
                            freeTextQuery = freeTextQuery.AsQueryable().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.Description.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.ScientificName.ToLower()
                                                                       .Contains(kvp.Value.ToLower()));
                        }

                    
                        break;

                    }

                    #endregion

                    #region plants

                        #region Time
                        case "Sowing":
                        {
                            var sowings = getMatchingTimePeriods(GetAll<Sowing>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.Sowing.Any(s => sowings.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.Sowing.Any(s => sowings.Contains(s)));

                            break;
                        }

                        case "Harvest":
                        {
                            var harvest = getMatchingTimePeriods(GetAll<Harvest>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.Harvest.Any(s => harvest.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.Harvest.Any(s => harvest.Contains(s)));

                            break;
                        }

                        case "Bloom":
                        {
                            var bloom = getMatchingTimePeriods(GetAll<Bloom>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.Bloom.Any(s => bloom.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.Bloom.Any(s => bloom.Contains(s)));

                            break;
                        }

                        case "SeedMaturity":
                        {
                            var seedMaturity = getMatchingTimePeriods(GetAll<SeedMaturity>(), TimePeriodHelper.GetMonth(Convert.ToInt32(kvp.Value)));

                            if (plantQuery == null)
                                plantQuery = session.Query<Plant>().Where(p => p.SeedMaturity.Any(s => seedMaturity.Contains(s)));
                            else
                                plantQuery = plantQuery.AsQueryable().Where(p => p.SeedMaturity.Any(s => seedMaturity.Contains(s)));

                            break;
                        }
                        #endregion

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
                    #endregion

                    #endregion
                }
            }

            if (freeTextQuery != null && plantQuery!=null)
                return (freeTextQuery.ToList().Intersect(plantQuery.ToList())).AsQueryable();


            if (freeTextQuery != null)
                return freeTextQuery;

            if (plantQuery != null)
                return plantQuery;


            return null;
        }


        private IEnumerable<TimePeriod> getMatchingTimePeriods(IEnumerable<TimePeriod> tps, TimePeriodMonth month)
        {
            List<TimePeriod> tempTimePeriods = new List<TimePeriod>();

            foreach (TimePeriod VARIABLE in tps)
            {
                if(VARIABLE.StartMonth <= month && VARIABLE.EndMonth >= month)
                    tempTimePeriods.Add(VARIABLE);
            }

            return tempTimePeriods;
        }

        #endregion
    }
}
