namespace MCT.DB.Entities.PatchPlaner
{

    public class Placement
    {
        public virtual long Id { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual string Transformation { get; set; }
        public virtual TimePeriodArea PlantingArea { get; set; }
        public virtual TimePeriodMonth PlantingMonth { get; set; }
        public virtual Patch Patch { get; set; }
    }
}
