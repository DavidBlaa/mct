using MCT.DB.Entities;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MCT.Web.Models
{
    public class PlantModel : NodeModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        [Display(Name = "Wurzeltiefe")]
        public RootDepth RootDepth { get; set; }
        [Display(Name = "Nähstoff Anspruch")]
        public NutrientClaim NutrientClaim { get; set; }
        [Display(Name = "Standort")]
        public LocationType LocationType { get; set; }

        [Display(Name = "Sähtiefe")]
        public int SowingDepth { get; set; }

        [UIHint("SimpleLinkModelList")]
        [Display(Name = "Vorkultur")]

        public virtual List<SimpleLinkModel> PreCultures { get; set; }
        [UIHint("SimpleLinkModelList")]
        [Display(Name = "Nachkultur")]
        public virtual List<SimpleLinkModel> AfterCultures { get; set; }

        public PlantModel()
        {
            LifeCycles = new List<List<TimePeriodModel>>();


            PreCultures = new List<SimpleLinkModel>();
            AfterCultures = new List<SimpleLinkModel>();
            Interactions = new List<InteractionModel>();
        }


        public static PlantModel Convert(Plant plant)
        {
            PlantModel model = new PlantModel();

            model.Id = plant.Id;

            #region subject
            if (!String.IsNullOrEmpty(plant.Name))
                model.Name = plant.Name;

            if (!String.IsNullOrEmpty(plant.Rank.ToString()))
                model.TaxonRank = plant.Rank;

            if (!String.IsNullOrEmpty(plant.Description))
                model.Description = plant.Description;

            model.Type = GetType(plant);

            if (plant.Medias.Count() == 0)
            {
                model.ImagePath = "/Images/Empty.png";
            }
            else
            {
                model.ImagePath = plant.Medias.First().ImagePath;
            }
            #endregion

            model.ScientificName = plant.ScientificName;
            model.Width = plant.Width;
            model.Height = plant.Height;
            model.NutrientClaim = plant.NutrientClaim;
            model.RootDepth = plant.RootDepth;
            model.LocationType = plant.LocationType;

            #region Dates


            if (model.LifeCycles != null)
            {
                model.LifeCycles = TimePeriodsToLifeCycles(plant.TimePeriods);
            }

            #endregion

            #region loadParentModels

            if (plant.Parent != null)
                model.Parent = SimpleNodeViewModel.Convert(plant.Parent);

            #endregion

            #region load pre/after cultures

            if (plant.PreCultures.Count > 0)
            {
                foreach (var p in plant.PreCultures)
                {
                    model.PreCultures.Add(new SimpleLinkModel(p.Id, p.Name, SubjectType.Plant));
                }
            }

            if (plant.AfterCultures.Count > 0)
            {
                foreach (var p in plant.AfterCultures)
                {
                    model.AfterCultures.Add(new SimpleLinkModel(p.Id, p.Name, SubjectType.Plant));
                }
            }

            #endregion

            return model;
        }

        private static List<string> GetTimePeriodsAsStringList<T>(ICollection<T> timeperiods) where T : TimePeriod
        {
            List<string> temp = new List<string>();

            if (timeperiods != null && timeperiods.Count > 0)
            {
                foreach (T tp in timeperiods)
                {
                    temp.Add(tp.GetTimePeriodAsString());
                }
            }

            return temp;
        }



    }
}