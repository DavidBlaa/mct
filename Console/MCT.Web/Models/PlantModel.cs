using MCT.DB.Entities;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MCT.Web.Models
{
    public class PlantModel : SpeciesModel
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

        [UIHint("TimePeriods")]
        [Display(Name = "Sähen")]
        public TimePeriodListModel Sowing { get; set; }
        [UIHint("TimePeriods")]
        [Display(Name = "Ernten")]
        public TimePeriodListModel Harvest { get; set; }
        [UIHint("TimePeriods")]
        [Display(Name = "Blühen")]
        public TimePeriodListModel Bloom { get; set; }
        [UIHint("TimePeriods")]
        [Display(Name = "Samen Reife")]
        public TimePeriodListModel SeedMaturity { get; set; }
        [UIHint("SimpleLinkModelList")]
        [Display(Name = "Vorkultur")]

        public virtual List<SimpleLinkModel> PreCultures { get; set; }
        [UIHint("SimpleLinkModelList")]
        [Display(Name = "Nachkultur")]
        public virtual List<SimpleLinkModel> AfterCultures { get; set; }

        public PlantModel()
        {
            Sowing = new TimePeriodListModel(TimePeriodType.Sowing);
            Harvest = new TimePeriodListModel(TimePeriodType.Harvest);
            Bloom = new TimePeriodListModel(TimePeriodType.Bloom);
            SeedMaturity = new TimePeriodListModel(TimePeriodType.SeedMaturity);
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

            if (plant.Sowing != null)
            {
                List<TimePeriod> tmp = new List<TimePeriod>();
                plant.Sowing.ForEach(s => tmp.Add((TimePeriod)s));

                model.Sowing = new TimePeriodListModel(tmp, TimePeriodType.Sowing);
            }

            if (plant.Bloom != null)
            {
                List<TimePeriod> tmp = new List<TimePeriod>();
                plant.Bloom.ForEach(s => tmp.Add((TimePeriod)s));

                model.Bloom = new TimePeriodListModel(tmp, TimePeriodType.Bloom);
            }

            if (plant.Harvest != null)
            {
                List<TimePeriod> tmp = new List<TimePeriod>();
                plant.Harvest.ForEach(s => tmp.Add((TimePeriod)s));

                model.Harvest = new TimePeriodListModel(tmp, TimePeriodType.Harvest);
            }

            if (plant.SeedMaturity != null)
            {
                List<TimePeriod> tmp = new List<TimePeriod>();
                plant.SeedMaturity.ForEach(s => tmp.Add((TimePeriod)s));

                model.SeedMaturity = new TimePeriodListModel(tmp, TimePeriodType.SeedMaturity);
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

    public class TimePeriodListModel
    {
        public TimePeriodType Type { get; set; }
        public List<TimePeriod> TimePeriods { get; set; }

        public TimePeriodListModel(List<TimePeriod> timePeriods, TimePeriodType type)
        {
            Type = type;
            TimePeriods = timePeriods;
        }

        public TimePeriodListModel(TimePeriodType type)
        {
            Type = type;
            TimePeriods = new List<TimePeriod>();
        }

        public TimePeriodListModel()
        {
            Type = TimePeriodType.Bloom;
            TimePeriods = new List<TimePeriod>();
        }
    }

}