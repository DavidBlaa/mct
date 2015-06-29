using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            model.Sowing = GetTimePeriodsAsStringList(plant.Sowing);
            model.Harvest = GetTimePeriodsAsStringList(plant.Harvest);
            model.Bloom = GetTimePeriodsAsStringList(plant.Bloom);
            model.SeedMaturity = GetTimePeriodsAsStringList(plant.SeedMaturity);


            #endregion

            return model;
        }

        private static List<string> GetTimePeriodsAsStringList(ICollection<TimePeriod> timeperiods)
        {
            List<string> temp = new List<string>();

            if (timeperiods != null && timeperiods.Count > 0)
            {
                foreach (TimePeriod tp in timeperiods)
                {
                    temp.Add(tp.GetTimePeriodAsString());
                }
            }

            return temp;
        }
    }
}