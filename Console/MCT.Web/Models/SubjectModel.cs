using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public SubjectType Type { get; set; }

        public SimpleNodeViewModel Parent { get; set; }

        [UIHint("Interactions")]
        public List<InteractionModel> Interactions { get; set; }

        public SubjectModel()
        {
            Name = "";
            Description = "";
            ImagePath = "/Images/Empty.png";
            Type = SubjectType.Unknow;
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



}