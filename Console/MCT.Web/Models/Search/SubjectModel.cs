using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Search;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using NHibernate.Proxy;

namespace MCT.Web.Models.Search
{
    public class SubjectModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ImagePath { get; set; }

        public SubjectType Type { get; set; }

        public List<InteractionModel> Interactions { get; set; }

        public static SubjectModel Convert(Subject subject)
        {

            SubjectModel model = new SubjectModel();

            model.Id = subject.Id;

            if (!String.IsNullOrEmpty(subject.Name))
                model.Name = subject.Name;

            if (!String.IsNullOrEmpty(subject.Description))
                model.Description = subject.Description;

            model.Type = GetType(subject);

            model.ImagePath = !subject.Medias.Any() ? "/Images/Empty.png" : subject.Medias.First().ImagePath;

            model.Interactions = new List<InteractionModel>();

            return model;
        }

        public static SubjectType GetType(Subject subject)
        {
            Type test ;

            if (subject.IsProxy())
            {
                subject = NHibernateHelper.UnProxyObjectAs<Subject>(subject);
            }

            if ((subject as Plant) != null) return SubjectType.Plant;
            if ((subject as Animal) !=null) return SubjectType.Animal;
            if ((subject as Effect) != null) return SubjectType.Effect;
            

            return SubjectType.Unknow;

        }

        public static List<InteractionModel> ConverInteractionModels(List<Interaction> interactions)
        {
            List<InteractionModel> interactionModels = new List<InteractionModel>();

            foreach (var i in interactions)
            {
                interactionModels.Add( InteractionModel.Convert(i));
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
        public InteractionElementModel Subject { get; set; }
        public InteractionElementModel Predicate { get; set; }
        public InteractionElementModel Object { get; set; }
        public InteractionElementModel ImpactSubject { get; set; }
        public Int32 Indicator { get; set; }

        public static InteractionModel Convert(Interaction interaction)
        {
            return new InteractionModel()
            {
                Subject = InteractionElementModel.Convert(interaction.Subject),
                Predicate = InteractionElementModel.Convert(interaction.Predicate),
                Object = InteractionElementModel.Convert(interaction.Object),
                ImpactSubject = InteractionElementModel.Convert(interaction.ImpactSubject),
                Indicator = interaction.Indicator,
            };
        }

    }

    public class InteractionElementModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }

        public static InteractionElementModel Convert(Subject subject)
        {
            if (subject != null)
                return new InteractionElementModel()
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    Type = SubjectModel.GetType(subject).ToString()
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