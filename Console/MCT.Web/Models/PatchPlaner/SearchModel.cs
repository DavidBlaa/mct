using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.PatchPlaner
{
    public class SearchModel
    {
        public ICollection<PlantModel> Plants { get; set; }
        //public FilterModel Filter { get; set; }
        public Dictionary<string, string> SearchCriterias { get; set; }

        public SearchModel()
        {
            Plants = new List<PlantModel>();
            //Filter = new FilterModel(new List<Subject>());
            SearchCriterias = new Dictionary<string, string>();
        }

    }
}