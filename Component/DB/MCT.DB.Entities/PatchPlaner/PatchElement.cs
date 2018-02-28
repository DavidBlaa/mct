using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities.PatchPlaner
{

    public class PatchElement
    {
        public virtual long Id { get; set; }
        public virtual string Transformation { get; set; }
        public virtual Patch Patch { get; set; }

    }
}
