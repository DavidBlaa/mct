using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Subject
    {
        #region Attributes

        public virtual long Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }

        #endregion

        #region Associations

        public virtual ICollection<Media> Medias { get; set; }

        #endregion 


        public Subject()
        {
            Medias = new List<Media>();
            Name = "";
            Description = "";
        }

        public static bool IsEmtpy(Subject subject)
        {
            if(subject.Id == 0)
                return true;
            else
                return false;
        }
    }
}
