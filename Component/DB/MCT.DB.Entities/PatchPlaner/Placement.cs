namespace MCT.DB.Entities.PatchPlaner
{

    public class Placement:PatchElement
    {
        public virtual Plant Plant { get; set; }
        public virtual TimePeriodArea PlantingArea { get; set; }
        public virtual TimePeriodMonth PlantingMonth { get; set; }
    }

}
