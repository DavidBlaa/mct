using MCT.DB.Entities;

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

        public PlacementModel()
        {
            Transformation = "matrix(1,0,0,1,0,0)";
        }
    }
}