using System;
using System.Collections.Generic;
using System.Linq;
using MCT.DB.Entities;

namespace MCT.Web.Models.Search
{
    public class PlantModel:SubjectModel
    {
        public string ScientificName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public String RootDepth { get; set; }
        public String NutrientClaim { get; set; }
        public int SowingDepth { get; set; }
        public List<String> Sowing { get; set; }
        public List<String> Harvest { get; set; }
        public List<String> Bloom { get; set; }
        public List<String> SeedMaturity { get; set; }

        public virtual List<SimpleLinkModel> PreCultures { get; set; }
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
            model.NutrientClaim = plant.NutrientClaim.ToString();
            model.RootDepth = plant.RootDepth.ToString();

            #region Dates

            if (plant.Sowing!=null) model.Sowing = GetTimePeriodsAsStringList(plant.Sowing);
            if (plant.Harvest != null) model.Harvest = GetTimePeriodsAsStringList(plant.Harvest);
            if (plant.Bloom != null) model.Bloom = GetTimePeriodsAsStringList(plant.Bloom);
            if (plant.SeedMaturity != null) model.SeedMaturity = GetTimePeriodsAsStringList(plant.SeedMaturity);


            #endregion

            #region loadParentModels

            if (plant.Parent!=null)
                model.Parent = SimpleNodeViewModel.Convert(plant.Parent);

            #endregion

            #region load pre/after cultures

                if (plant.PreCultures.Count > 0)
                {
                    foreach (var p in plant.PreCultures)
                    { 
                        model.PreCultures.Add(new SimpleLinkModel(p.Id,p.Name, SubjectType.Plant.ToString()));
                    }
                }

                if (plant.AfterCultures.Count > 0)
                {
                    foreach (var p in plant.AfterCultures)
                    {
                        model.AfterCultures.Add(new SimpleLinkModel(p.Id,p.Name, SubjectType.Plant.ToString()));
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