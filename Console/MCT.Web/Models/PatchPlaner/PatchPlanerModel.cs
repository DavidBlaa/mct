using MCT.Web.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.PatchPlaner
{
    public class PatchPlanerModel
    {
        public PatchModel Patch { get; set; }
        public SearchModel Search { get; set; }
    }
}