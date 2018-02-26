using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.PatchPlaner
{
    public class PatchModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public LocationType LocationType { get; set; }
        public NutrientClaim NutrientClaim { get; set; }

        public ICollection<PlacementModel> Placements { get; set; }

    }
}