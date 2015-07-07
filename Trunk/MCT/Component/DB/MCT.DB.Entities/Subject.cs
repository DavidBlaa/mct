﻿using System;
using System.Collections.Generic;

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
            return false;
        }
    }
}