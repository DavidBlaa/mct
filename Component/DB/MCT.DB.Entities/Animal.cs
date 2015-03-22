using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Animal:Subject
    {
        //Name und Hierarchie
        public virtual string ScientificName { get; set; }
        public virtual string ClassName { get; set; }
        public virtual string OrderName { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string GenusName { get; set; }
    }
}
