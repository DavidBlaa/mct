using System.Collections.Generic;
using System.Linq;
using MCT.DB.Entities;
using MCT.DB.Services;

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
            SubjectManager subjectManager = new SubjectManager();
            return subjectManager.GetAll<Species>().Where(s => s.Name.ToLower().Contains(searchValue.ToLower()) 
                                                            || s.Description.ToLower().Contains(searchValue.ToLower())
                                                            || s.ScientificName.ToLower().Contains(searchValue.ToLower()));
        }
    }
}
