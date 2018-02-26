using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.PatchPlaner
{
    public class PlacementModel
    {
        public virtual long Id { get; set; }
        public virtual PlantModel Plant { get; set; }
        public virtual string Transformation { get; set; }
        public virtual TimePeriodArea PlantingArea { get; set; }
        public virtual TimePeriodMonth PlantingMonth { get; set; }
        public virtual long PatchId { get; set; }
    }
}