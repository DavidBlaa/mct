using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.Search
{
    public class AnimalModel:SubjectModel
    {
        public string ScientificName { get; set; }

        public static AnimalModel Convert(Animal animal)
        {
            AnimalModel model = new AnimalModel();

            model.Id = animal.Id;

            #region subject
            if (!String.IsNullOrEmpty(animal.Name))
                model.Name = animal.Name;

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

            return model;
        }
    }
}