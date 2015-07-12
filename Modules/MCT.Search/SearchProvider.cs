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
using NHibernate.Search;

namespace MCT.Search
{
    public class SearchProvider
    {
        /// <summary>
        /// search value in name and descrtiption and scientific name
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static IEnumerable<Species> Search(string searchValue)
        {
            //SubjectManager subjectManager = new SubjectManager();
            //return subjectManager.GetAllAsQueryable<Species>().Where(s => s.Name.ToLower().Contains(searchValue.ToLower())
            //                                                || s.Description.ToLower().Contains(searchValue.ToLower())
            //                                                || s.ScientificName.ToLower().Contains(searchValue.ToLower()));

            List<Species> speciesList = new List<Species>();
            string searchQuery = "";
            try
            {
                if (String.IsNullOrEmpty(searchValue))
                    searchQuery = "*:*";
                else
                    searchQuery = string.Format("Name:{0}* OR Description:{0}*", searchValue);

                Analyzer std = new KeywordAnalyzer();
                QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "1", std);
                Query query = parser.Parse(searchQuery);

                IFullTextSession fullTextSession = NHibernate.Search.Search.CreateFullTextSession(NHibernateHelper.GetCurrentSession());
                IFullTextQuery fullTextQuery = fullTextSession.CreateFullTextQuery(query, typeof(Species));


                if (fullTextQuery != null)
                    speciesList.AddRange(fullTextQuery.List().Cast<Species>());
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error in searching in index: " + exception.Message);
                throw new Exception("Error in searching in index: " + exception.Message);
            }

            return speciesList;
            
        }

        public static void ReIndex()
        {
            NHibernateHelper.ReIndex();
        }
    }
}
