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
        [Field(Index.UnTokenized, Name = "SubjectName")]
        public virtual String Name { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        [Field(Index.UnTokenized, Name = "SubjectDescription")]
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
