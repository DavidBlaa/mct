using System.Collections.Generic;


namespace MCT.DB.Entities.PatchPlaner
{

    public class Patch
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public virtual LocationType LocationType { get; set; }
        public virtual NutrientClaim NutrientClaim { get; set; }

        public virtual ICollection<Placement> Placements { get; set; }
    }

}
