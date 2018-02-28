namespace MCT.DB.Entities.PatchPlaner
{

    public class PatchElement
    {
        public virtual long Id { get; set; }
        public virtual string Transformation { get; set; }
        public virtual Patch Patch { get; set; }

        public PatchElement()
        {
            Transformation = "matrix(1,0,0,1,0,0)";
        }

    }
}
