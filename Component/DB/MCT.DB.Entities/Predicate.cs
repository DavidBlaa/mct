using System;
using System.Collections.Generic;

namespace MCT.DB.Entities
{
    public class Predicate
    {
        public virtual long Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual Predicate Parent { get; set; }
    }
}
