using MCT.DB.Entities;
using MCT.Web.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class AnimalModel : NodeModel
    {
        public AnimalModel()
        {
        }

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
                model.ImagePath = animal.Medias.Last().ImagePath;
            }

            if (model.LifeCycles != null)
            {
                model.LifeCycles = TimePeriodsToLifeCycles(animal.TimePeriods);
            }

            #endregion subject

            model.ScientificName = animal.ScientificName;

            #region loadParentModels

            if (animal.Parent != null)
                model.Parent = SimpleNodeViewModel.Convert(animal.Parent);

            #endregion loadParentModels

            //load children
            model.Childrens = ModelHelper.GetChildren(animal.Id);

            return model;
        }
    }
}