using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
