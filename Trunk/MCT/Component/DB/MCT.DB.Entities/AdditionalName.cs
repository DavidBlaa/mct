using System;

namespace MCT.DB.Entities
{
    public class AdditionalName
    {
        public virtual long Id { get; set; }
        public virtual long NodeId { get; set; }
        public virtual String Name { get; set; }
        public virtual String Language { get; set; }
    }
}
