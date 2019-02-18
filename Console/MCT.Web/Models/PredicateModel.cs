using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class PredicateModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Beschreibung")]
        public string Description { get; set; }

        public PredicateModel Parent { get; set; }

        public PredicateModel()
        {
            Id = 0;
            Name = "";
            Description = "";
            Parent = null;
        }

        public static PredicateModel Convert(Predicate predicate)
        {
            PredicateModel model = new PredicateModel();
            model.Id = predicate.Id;
            model.Name = predicate.Name;
            model.Description = predicate.Description;

            if (predicate.Parent != null)
            {
                model.Parent = PredicateModel.Convert(predicate.Parent);
            }

            return model;
        }

        public static Predicate Convert(PredicateModel predicate)
        {
            Predicate model = new Predicate();
            model.Id = predicate.Id;
            model.Name = predicate.Name;
            model.Description = predicate.Description;

            if (predicate.Parent != null)
            {
                model.Parent = PredicateModel.Convert(predicate.Parent);
            }

            return model;
        }
    }

    public class PredicateCreateModel
    {
        public long Id { get; set; }

        [Required]
        [Remote("CheckPredicate", "Predicate", ErrorMessage = "Prädikat existiert bereits.", AdditionalFields = "initName")]
        public string Name { get; set; }

        [Display(Name = "Beschreibung")]
        public string Description { get; set; }

        public long ParentId { get; set; }
    }
}