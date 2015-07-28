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

            IQueryable<Plant> query = null;

            foreach (KeyValuePair<string, string> kvp in searchCriteria)
            {
                switch (kvp.Key)
                {
                    case "FREETEXT_SEARCH_KEY":
                    {

                        if (query == null)
                        {
                            query = session.Query<Plant>().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.Description.ToLower()
                                                                          .Contains(kvp.Value.ToLower())
                                                                      ||
                                                                      s.ScientificName.ToLower()
                                                                          .Contains(kvp.Value.ToLower()));
                        }
                        else
                        {
                            query = query.AsQueryable().Where(s => s.Name.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.Description.ToLower().Contains(kvp.Value.ToLower())
                                                                   ||
                                                                   s.ScientificName.ToLower()
                                                                       .Contains(kvp.Value.ToLower()));
                        }

                        break;
                    }


                    case "Sowing":
                        {
                            var sowings = GetAll<Sowing>().Where(s => s.StartMonth.Equals(TimePeriodHelper.GetMonth(kvp.Value)) || s.EndMonth.Equals(TimePeriodHelper.GetMonth(kvp.Value)));

                            if (query == null)
                            {
                                query = session.Query<Plant>().Where(p => p.Sowing.Any(s => sowings.Contains(s)));
                            }
                            else
                            {
                                query = query.AsQueryable().Where(p => p.Sowing.Any(s => sowings.Contains(s)));
                            }

                            break;
                        }

                }
            }

            return query;
        }

        #endregion
    }
}
