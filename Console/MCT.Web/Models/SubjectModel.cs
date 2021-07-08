using MCT.DB.Entities;
using MCT.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class SubjectModel
    {
        public long Id { get; set; }

        [Required]
        [Remote("CheckNameExist", "Subject", ErrorMessage = "Name existiert bereits.", AdditionalFields = "initName")]
        public String Name { get; set; }

        public String Description { get; set; }
        public String ImagePath { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public SubjectType Type { get; set; }

        
        public List<SimpleNodeViewModel> Childrens { get; set; }

        [UIHint("Interactions")]
        public List<InteractionModel> Interactions { get; set; }

        [UIHint("LifeCycles")]
        [Display(Name = "Lebenszyklus")]
        public List<List<TimePeriodModel>> LifeCycles { get; set; }

        public SubjectModel()
        {
            Name = "";
            Description = "";
            ImagePath = "/Images/Empty.png";
            Type = SubjectType.Unknow;
            LifeCycles = new List<List<TimePeriodModel>>();
        }

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

            model.LifeCycles = new List<List<TimePeriodModel>>();

        
            if (subject.TimePeriods != null)
            {
                model.LifeCycles = TimePeriodsToLifeCycles(subject.TimePeriods);
            }

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
            if ((subject as Taxon) != null) return SubjectType.Taxon;

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

        public static List<List<TimePeriodModel>> TimePeriodsToLifeCycles(ICollection<TimePeriod> timeperiods)
        {
            List<List<TimePeriodModel>> tmp = new List<List<TimePeriodModel>>();

            List<TimePeriod> startPoints = timeperiods.Where(tp => tp.Start == true).ToList();

            foreach (var item in startPoints)
            {
                List<TimePeriodModel> lifeCyle = convertAndAddTimePeriodModel(item, new List<TimePeriodModel>());
                tmp.Add(lifeCyle);
            }

            return tmp;
        }

        private static List<TimePeriodModel> convertAndAddTimePeriodModel(TimePeriod timeperiod, List<TimePeriodModel> lifeCycle)
        {
            lifeCycle.Add(new TimePeriodModel(timeperiod));

            if (timeperiod.Next != null)
            {
                convertAndAddTimePeriodModel(timeperiod.Next, lifeCycle);
            }

            return lifeCycle;
        }
    }

    [DataContract]
    public enum SubjectType
    {
        [Display(Name = "Tier")]
        [EnumMember(Value = "Tier")]
        Animal,

        [Display(Name = "Effekt")]
        [EnumMember(Value = "Effekt")]
        Effect,

        [Display(Name = "Pflanze")]
        [EnumMember(Value = "Pflanze")]
        Plant,

        [Display(Name = "Taxon")]
        [EnumMember(Value = "Taxon")]
        Taxon,

        [Display(Name = "Unbekannt")]
        [EnumMember(Value = "Unbekannt")]
        Unknow
    }
}