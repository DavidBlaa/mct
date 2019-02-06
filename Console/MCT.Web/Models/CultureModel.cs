using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class CultureModel
    {
        public long Id { get; set; }

        [Required]
        [Remote("CheckNameOfSimpleLink", "Subject", ErrorMessage = "Name existiert nicht.")]
        public String Name { get; set; }

        [Required]
        public SubjectType Type { get; set; }

        public CultureModel()
        {
            Id = 0;
            Name = string.Empty;
            Type = SubjectType.Unknow;
        }

        public CultureModel(long id, string name, SubjectType type)
        {
            Id = id;
            Name = name;
            Type = SubjectType.Unknow;
        }
    }
}