using System.Collections.Generic;

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