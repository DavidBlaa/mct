using MCT.DB.Entities;
using System;
using System.Linq;

namespace MCT.Web.Models
{
    public class AnimalModel : NodeModel
    {
        public static AnimalModel Convert(Animal animal)
        {
            AnimalModel model = new AnimalModel();

            model.Id = animal.Id;

            #region subject
            if (!String.IsNullOrEmpty(animal.Name))
                model.Name = animal.Name;

            if (!String.IsNullOrEmpty(animal.Rank.ToString()))
                model.TaxonRank = animal.Rank;


            if (!String.IsNullOrEmpty(animal.Description))
                model.Description = animal.Description;

            model.Type = GetType(animal);

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
            if(animal.Parent!=null)
            model.Parent = SimpleNodeViewModel.Convert(animal.Parent);

            #endregion

            return model;
        }
    }
}