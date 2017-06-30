using MCT.DB.Entities;
using System;
using System.Linq;

namespace MCT.Web.Models
{
    public class AnimalModel : SubjectModel
    {
        public string ScientificName { get; set; }
        public string TaxonRank { get; set; }

        public static AnimalModel Convert(Animal animal)
        {
            AnimalModel model = new AnimalModel();

            model.Id = animal.Id;

            #region subject
            if (!String.IsNullOrEmpty(animal.Name))
                model.Name = animal.Name;

            if (!String.IsNullOrEmpty(animal.Rank.ToString()))
                model.TaxonRank = animal.Rank.ToString();


            if (!String.IsNullOrEmpty(animal.Description))
                model.Description = animal.Description;

            model.SubjectType = GetType(animal);

            if (animal.Medias.Count() == 0)
            {
                model.ImagePath = "/Images/Empty.png";
            }
            else
            {
                model.ImagePath = animal.Medias.First().ImagePath;
            }
            #endregion

            model.ScientificName = animal.ScientificName;

            #region loadParentModels

            model.Parent = SimpleNodeViewModel.Convert(animal.Parent);

            #endregion

            return model;
        }
    }
}