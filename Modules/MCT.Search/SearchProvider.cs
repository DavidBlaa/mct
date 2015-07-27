using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using NHibernate.Properties;
using NHibernate.Search;
using NHibernate.Util;

namespace MCT.Search
{
    public class SearchProvider
    {
        public static string SEARCH_PROVIDER_NAME = "SEARCH_PROVIDER_NAME";
        public static string FREETEXT_SEARCH_KEY = "FREETEXT_SEARCH_KEY";
        public static string SOWING = "Sowing";
        public static string HARVEST = "Harvest";
        public static string BLOOM = "Bloom";
        public static string SEED_MATURITY = "Seed Maturity";

        public Dictionary<string, string> SearchCriterias { get; set; }

        private SubjectManager _manager;

        public SearchProvider()
        {
            SearchCriterias = new Dictionary<string, string>();
        }

        public IEnumerable<Species> Search()
        {

            _manager = new SubjectManager();

            object results = new object();

            if (SearchCriterias.Any())
            {
                // suchanfrage beginnt
                if (SearchCriterias.ContainsKey(FREETEXT_SEARCH_KEY))
                {
                    results = search(SearchCriterias[FREETEXT_SEARCH_KEY]);
                }

                if (SearchCriterias.ContainsKey(SOWING))
                {
                    results = searchTimeFilter(SOWING, SearchCriterias[SOWING]);
                }

                return ((IQueryable<Species>)results).ToList();

            }

            return new List<Species>();
        }

        public void UpateSearchCriterias(string key, string value)
        {
            if (SearchCriterias.ContainsKey(key))
                SearchCriterias[key] = value;
            else
                SearchCriterias.Add(key, value);
        }

        public void DeleteSearchCriterias(string key)
        {
            if (SearchCriterias.ContainsKey(key))
                SearchCriterias.Remove(key);

        }

        /// <summary>
        /// search value in name and descrtiption and scientific name
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        private IQueryable<Species> search(string searchValue)
        {
            _manager = new SubjectManager();

            if (string.IsNullOrEmpty(searchValue))
                return null;

            return _manager.GetAllAsQueryable<Species>().Where(s => s.Name.ToLower().Contains(searchValue.ToLower())
                                                            || s.Description.ToLower().Contains(searchValue.ToLower())
                                                            || s.ScientificName.ToLower().Contains(searchValue.ToLower()));
        }


        private IQueryable<Species> searchTimeFilter(string key, string value)
        {
            switch (key)
            {
                case "Sowing":
                    {
                        IEnumerable<Sowing> sowingMatchesList =
                            _manager.GetAll<Sowing>().Where(s => s.EndMonth.ToString().Equals(value) || s.StartMonth.ToString().Equals(value)).ToList();

                        if (sowingMatchesList.Any())
                        {
                            var plants = from plant in _manager.GetAllAsQueryable<Plant>()
                                         where plant.Sowing.Any(s => sowingMatchesList.Select(e => e.Id.Equals(s.Id)).Any())
                                         select plant;

                            return plants.AsQueryable();
                        }

                        break;
                    }

                default:
                    {
                        return null;
                    }
            }

            return null;
        }

        //GetName(new { var1 });
        private string GetName<T>(T item) where T : class
        {
            return typeof(T).GetProperties()[0].Name;
        }

        #region Lucene Index

        public static void ReIndex()
        {
            NHibernateHelper.ReIndex();
        }

        #endregion
    }
}
