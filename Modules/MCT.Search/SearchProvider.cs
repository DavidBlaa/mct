using MCT.DB.Entities;
using MCT.DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.Search
{
    public class SearchProvider
    {
        /// <summary>
        /// search value in name and descrtiption
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static IEnumerable<Subject> Search(string searchValue)
        {
            SubjectManager subjectManager = new SubjectManager();
            return subjectManager.GetAll<Subject>().Where(s => s.Name.Equals(searchValue) || s.Description.Contains(searchValue));
        }
    }
}
