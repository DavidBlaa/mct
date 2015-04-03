using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Node:Subject
    {
        public virtual String ScientificName { get; set; }
        public virtual TaxonType Type { get; set; }
        public virtual Node Parent { get; set; }

    }

    public enum TaxonType
    { 
        Class,
        Family,
        Genus,
        Kingdom,
        Order,
        Trunk
    }
}
