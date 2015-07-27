using System.Collections.Generic;

namespace MCT.Web.Models.Search
{
    public class SearchModel
    {
        public ICollection<SpeciesModel> Species { get; set; }
        public FilterModel Filter { get; set; }
        public Dictionary<string, string> SearchCriterias { get; set; }

        public SearchModel()
        {
            Species = new List<SpeciesModel>();
            Filter = new FilterModel();
            SearchCriterias = new Dictionary<string, string>();
        }
    }
}