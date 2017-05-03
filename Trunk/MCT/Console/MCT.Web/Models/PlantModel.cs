using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MCT.Web.Models
{
    public class PlantModel : SubjectModel
    {
        public string ScientificName { get; set; }
        public TaxonRank TaxonRank { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public RootDepth RootDepth { get; set; }
        public NutrientClaim NutrientClaim { get; set; }
        public int SowingDepth { get; set; }

        [UIHint("SowingList")]
        public List<Sowing> Sowing { get; set; }
        [UIHint("HarvestList")]
        public List<Harvest> Harvest { get; set; }
        [UIHint("BloomList")]
        public List<Bloom> Bloom { get; set; }
        [UIHint("SeedMaturityList")]
        public List<SeedMaturity> SeedMaturity { get; set; }

        [UIHint("SimpleLinkModelList")]
        public virtual List<SimpleLinkModel> PreCultures { get; set; }
        [UIHint("SimpleLinkModelList")]
        public virtual List<SimpleLinkModel> AfterCultures { get; set; }

        public PlantModel()
        {
            PreCultures = new List<SimpleLinkModel>();
            AfterCultures = new List<SimpleLinkModel>();
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

            #region Dates

            if (plant.Sowing != null) model.Sowing = plant.Sowing.ToList();
            if (plant.Harvest != null) model.Harvest = plant.Harvest.ToList();
            if (plant.Bloom != null) model.Bloom = plant.Bloom.ToList();
            if (plant.SeedMaturity != null) model.SeedMaturity = plant.SeedMaturity.ToList();


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