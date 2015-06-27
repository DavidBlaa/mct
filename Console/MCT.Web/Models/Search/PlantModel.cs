using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.Search
{
    public class PlantModel:SubjectModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public String RootDepth { get; set; }
        public String NutrientClaim { get; set; }
        public int SowingDepth { get; set; }
        List<String> Sowing { get; set; }

        public static PlantModel Convert(Plant plant)
        {
            PlantModel model = new PlantModel();

            model.Id = plant.Id;

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

            model.Width = plant.Width;
            model.Height = plant.Height;
            model.NutrientClaim = plant.NutrientClaim.ToString();
            model.RootDepth = plant.RootDepth.ToString();

            //sowing to string list
            model.Sowing = new List<string>();
            model.Sowing.Add("test januar");

            return model;
        }



    }
}