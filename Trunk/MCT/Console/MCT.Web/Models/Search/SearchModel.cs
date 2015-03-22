using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.Search
{
    public class SearchModel
    {
        public ICollection<SubjectModel> Subjects { get; set; }

        public SearchModel()
        {
            Subjects = new List<SubjectModel>();
        }
    }
}