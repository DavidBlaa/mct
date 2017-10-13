using NHibernate.Search.Attributes;
using System;
using System.Collections.Generic;

namespace MCT.DB.Entities
{
    [Indexed]
    public class Subject
    {
        #region Attributes
        [DocumentId]
        public virtual long Id { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual String Name { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual String Description { get; set; }

        #endregion

        #region Associations

        public virtual ICollection<Media> Medias { get; set; }

        //Dates
        public virtual ICollection<TimePeriod> TimePeriods { get; set; }

        #endregion 


        public Subject()
        {
            TimePeriods = new List<TimePeriod>();
            Medias = new List<Media>();
            Name = "";
            Description = "";
        }

        public static bool IsEmtpy(Subject subject)
        {
            if (subject.Id == 0)
                return true;
            return false;
        }
    }
}
