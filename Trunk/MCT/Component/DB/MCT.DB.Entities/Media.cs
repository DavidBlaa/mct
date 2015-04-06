using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Media
    {
        public virtual long Id { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual String ImagePath { get; set; }
        public virtual String MIMEType { get; set; }

        public Media()
        {
            ImagePath = "";
            MIMEType = "";
        }
    }
}
