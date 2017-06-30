using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MCT.Web.Models
{
    public class SubjectModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ImagePath { get; set; }

        public SubjectType SubjectType { get; set; }

        public SimpleNodeViewModel Parent { get; set; }

        [UIHint("Interactions")]
        public List<InteractionModel> Interactions { get; set; }

        public SubjectModel()
        {
            ImagePath = "/Images/Empty.png";
        }

        public static SubjectModel Convert(Subject subject)
        {

            SubjectModel model = new SubjectModel();

            model.Id = subject.Id;

            if (!String.IsNullOrEmpty(subject.Name))
                model.Name = subject.Name;

            if (!String.IsNullOrEmpty(subject.Description))
                model.Description = subject.Description;

            model.SubjectType = GetType(subject);

            model.ImagePath = !subject.Medias.Any() ? "/Images/Empty.png" : subject.Medias.First().ImagePath;

            model.Interactions = new List<InteractionModel>();

            return model;
        }

        public static SubjectType GetType(Subject subject)
        {
            if (subject.IsProxy())
            {
                subject = NHibernateHelper.UnProxyObjectAs<Subject>(subject);
            }

            if ((subject as Plant) != null) return SubjectType.Plant;
            if ((subject as Animal) != null) return SubjectType.Animal;
            if ((subject as Effect) != null) return SubjectType.Effect;


            return SubjectType.Unknow;

        }

        public static List<InteractionModel> ConverInteractionModels(List<Interaction> interactions)
        {
            List<InteractionModel> interactionModels = new List<InteractionModel>();

            foreach (var i in interactions)
            {
                interactionModels.Add(InteractionModel.Convert(i));
            }

            return interactionModels;
        }
    }

    public enum SubjectType
    {
        Animal,
        Effect,
        Plant,
        Unknow
    }

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

    public class SimpleLinkModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
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
        public String Name { get; set; }
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