using MCT.DB.Entities;
using System;
using System.Linq;

namespace MCT.Web.Models
{
    public class SpeciesModel:SubjectModel
    {
        public TaxonRank TaxonRank { get; set; }
        public String ScientificName { get; set; }
        //public SpeciesType Type { get; set; }

        public SpeciesModel()
        {
            ScientificName = "";
            TaxonRank = TaxonRank.Species;
        }


        public static SpeciesModel Convert(Species species)
        {
            SpeciesModel model = new SpeciesModel();

            model.Id = species.Id;
            model.ScientificName = species.ScientificName;
            model.TaxonRank = species.Rank;
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
                model.ImagePath = species.Medias.First().ImagePath;
            }

            return model;
        }

        //protected static SpeciesType GetType(Species species)
        //{
        //    if (species is Animal) return SpeciesType.Animal;
        //    if (species is Plant) return SpeciesType.Plant;

        //    return SpeciesType.Unknow;

        //}
    }

    public enum SpeciesType
    {
        Animal,
        Plant,
        Unknow
    }
}