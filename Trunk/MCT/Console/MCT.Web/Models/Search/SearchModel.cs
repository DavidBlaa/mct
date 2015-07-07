using System.Collections.Generic;

namespace MCT.Web.Models.Search
{
    public class SearchModel
    {
        public ICollection<SpeciesModel> Species { get; set; }

        public SearchModel()
        {
            Species = new List<SpeciesModel>();
        }
    }
}