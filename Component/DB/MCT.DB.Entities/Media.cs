using System;

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
