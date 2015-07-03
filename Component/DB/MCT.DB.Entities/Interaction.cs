namespace MCT.DB.Entities
{
    public class Interaction
    {
        public virtual long Id { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Predicate Predicate { get; set; }
        public virtual Subject Object { get; set; }
        public virtual Subject ImpactSubject { get; set; }
        public virtual int Indicator { get; set; }
        
    }
}
