using MCT.DB.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class InteractionModel
    {
        public long Id { get; set; }
        public InteractionElementModel Subject { get; set; }
        public InteractionPredicateModel Predicate { get; set; }
        public InteractionElementModel Object { get; set; }
        public InteractionElementModel ImpactSubject { get; set; }
        public Int32 Indicator { get; set; }

        public InteractionModel()
        {
            Id = 0;
            Indicator = 0;
            Subject = new InteractionElementModel();
            Object = new InteractionElementModel();
            ImpactSubject = new InteractionElementModel();
            Predicate = new InteractionPredicateModel();
        }

        public static InteractionModel Convert(Interaction interaction)
        {
            return new InteractionModel()
            {
                Id = interaction.Id,
                Subject = InteractionElementModel.Convert(interaction.Subject),
                Predicate = InteractionPredicateModel.Convert(interaction.Predicate),
                Object = InteractionElementModel.Convert(interaction.Object),
                ImpactSubject = InteractionElementModel.Convert(interaction.ImpactSubject),
                Indicator = interaction.Indicator,
            };
        }

    }

    public class InteractionSimpleModel
    {
        public long Id { get; set; }
        [Required]
        [Remote("CheckSubject", "Interaction", ErrorMessage = "Subject existiert nicht.")]
        public string Subject { get; set; }
        [Required]
        [Remote("CheckPredicate", "Interaction", ErrorMessage = "Predicate existiert nicht.")]
        public string Predicate { get; set; }
        [Required]
        [Remote("CheckObject", "Interaction", ErrorMessage = "Object existiert nicht.")]
        public string Object { get; set; }

        [Remote("CheckImpactSubject", "Interaction", ErrorMessage = "Impact Subject existiert nicht.")]
        public string ImpactSubject { get; set; }
        public Int32 Indicator { get; set; }


        public InteractionSimpleModel()
        {
            Id = 0;
            Indicator = 0;
            Subject = "";
            Object = "";
            ImpactSubject = "";
            Predicate = "";
        }

        public static InteractionSimpleModel Convert(Interaction interaction)
        {
            return new InteractionSimpleModel()
            {
                Id = interaction.Id,
                Subject = interaction.Subject == null ? "" : interaction.Subject.Name,
                Predicate = interaction.Predicate == null ? "" : interaction.Predicate.Name,
                Object = interaction.Object == null ? "" : interaction.Object.Name,
                ImpactSubject = interaction.ImpactSubject == null ? "" : interaction.ImpactSubject.Name,
                Indicator = interaction.Indicator,
            };
        }

    }

    public class SimpleLinkModel
    {
        public long Id { get; set; }
        [Required]
        [Remote("CheckNameOfSimpleLink", "Interaction", ErrorMessage = "Name existiert nicht.")]
        public String Name { get; set; }
        [Required]
        public SubjectType Type { get; set; }

        public SimpleLinkModel()
        {
            Id = 0;
            Name = string.Empty;
            Type = SubjectType.Unknow;
        }

        public SimpleLinkModel(long id, string name, SubjectType type)
        {
            Id = id;
            Name = name;
            Type = SubjectType.Unknow;
        }
    }

    public class InteractionPredicateModel
    {

        public long Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public string ParentName { get; set; }

        public InteractionPredicateModel()
        {
            Id = 0;
            Name = "";
            ParentName = "";
        }

        public static InteractionPredicateModel Convert(Predicate predicate)
        {
            string pName = "";
            if (predicate.Parent != null)
                pName = predicate.Parent.Name;

            return new InteractionPredicateModel
            {
                Id = predicate.Id,
                Name = predicate.Name,
                ParentName = pName
            };
        }
    }

    public class InteractionElementModel : SimpleLinkModel
    {

        public static InteractionElementModel Convert(Subject subject)
        {
            if (subject != null)
                return new InteractionElementModel()
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    Type = SubjectModel.GetType(subject)
                };

            return null;
        }

        public static InteractionElementModel Convert(Predicate predicate)
        {
            if (predicate != null)
                return new InteractionElementModel()
                {
                    Id = predicate.Id,
                    Name = predicate.Name,
                };

            return null;
        }
    }
}