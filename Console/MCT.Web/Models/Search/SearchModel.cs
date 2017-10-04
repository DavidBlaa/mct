using System.Collections.Generic;
using MCT.DB.Entities;

namespace MCT.Web.Models.Search
{
    public class SearchModel
    {
        public ICollection<NodeModel> Species { get; set; }
        public FilterModel Filter { get; set; }
        public Dictionary<string, string> SearchCriterias { get; set; }

        public SearchModel()
        {
            Species = new List<NodeModel>();
            Filter = new FilterModel(new List<Subject>());
            SearchCriterias = new Dictionary<string, string>();
        }

        public SearchModel(List<Subject> subjects)
        {
            Species = new List<NodeModel>();
            Filter = new FilterModel(subjects);
            SearchCriterias = new Dictionary<string, string>();
        }
    }
}