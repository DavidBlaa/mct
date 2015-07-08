using System;
using System.Linq;
using MCT.DB.Entities;

namespace MCT.Web.Models.Search
{
    public class SpeciesModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String ScientificName { get; set; }
        public String Description { get; set; }
        public String ImagePath { get; set; }

        public SpeciesType Type { get; set; }

        public static SpeciesModel Convert(Species species)
        {

            SpeciesModel model = new SpeciesModel();

            //SubjectLevel

            model.Id = species.Id;

            if (!String.IsNullOrEmpty(species.Name))
                model.Name = species.Name;

            if (!String.IsNullOrEmpty(species.Description))
                model.Description = species.Description;

            model.Type = GetType(species);

            if (!species.Medias.Any())
            {
                model.ImagePath = "/Images/Empty.png";
            }
            else
            {
                model.ImagePath =  species.Medias.First().ImagePath;
            }

            // Species Level
            model.ScientificName = species.ScientificName;


            return model;
        }

        protected static SpeciesType GetType(Species species)
        {
            if (species is Animal) return SpeciesType.Animal;
            if(species is Plant) return SpeciesType.Plant;

            return SpeciesType.Unknow;

        }
    }

    public enum SpeciesType
    { 
        Animal,
        Plant,
        Unknow
    }
}