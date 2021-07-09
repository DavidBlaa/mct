using MCT.DB.Entities;
using MCT.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MCT.Web.Models
{
    public class PlantModel : NodeModel
    {
        [Display(Name = "Breite")]
        public double Width { get; set; }

        [Display(Name = "Tiefe")]
        public double Height { get; set; }

        [Display(Name = "Wurzeltiefe")]
        public RootDepth RootDepth { get; set; }

        [Display(Name = "Nähstoff Anspruch")]
        [JsonConverter(typeof(StringEnumConverter))]

        public NutrientClaim NutrientClaim { get; set; }

        [Display(Name = "Standort")]
        [JsonConverter(typeof(StringEnumConverter))]

        public LocationType LocationType { get; set; }

        [Display(Name = "Sähtiefe")]
        public int SowingDepth { get; set; }

        [UIHint("CultureModelList")]
        [Display(Name = "Vorkultur")]
        public virtual List<CultureModel> PreCultures { get; set; }

        [UIHint("CultureModelList")]
        [Display(Name = "Nachkultur")]
        public virtual List<CultureModel> AfterCultures { get; set; }

        public PlantModel()
        {
            LifeCycles = new List<List<TimePeriodModel>>();

            PreCultures = new List<CultureModel>();
            AfterCultures = new List<CultureModel>();
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
                model.ImagePath = plant.Medias.Last().ImagePath;
            }

            #endregion subject

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

            #endregion Dates

            #region loadParentModels

            if (plant.Parent != null)
                model.Parent = SimpleNodeViewModel.Convert(plant.Parent);

            #endregion loadParentModels

            #region load pre/after cultures

            if (plant.PreCultures.Count > 0)
            {
                foreach (var p in plant.PreCultures)
                {
                    model.PreCultures.Add(new CultureModel(p.Id, p.Name, SubjectType.Plant));
                }
            }
            else
            {
                model.PreCultures.Add(new CultureModel());
            }

            if (plant.AfterCultures.Count > 0)
            {
                foreach (var p in plant.AfterCultures)
                {
                    model.AfterCultures.Add(new CultureModel(p.Id, p.Name, SubjectType.Plant));
                }
            }
            else
            {
                model.AfterCultures.Add(new CultureModel());
            }

            #endregion load pre/after cultures

            //load children
            model.Childrens = ModelHelper.GetChildren(plant.Id);


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