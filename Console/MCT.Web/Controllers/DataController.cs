using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCT.DB.Entities;
using MCT.Search;
using MCT.DB.Services;
using MCT.Helpers;
using MCT.IO;
using MCT.Web.Helpers;

namespace MCT.Web.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReIndex()
        {
            SearchProvider.ReIndex();

            return View("Index");
        }

        #region load Data


        public ActionResult LoadData()
        {
            // test create seeddata

            SeedDataGenerator.GenerateDays();

            AsciiReader reader = new AsciiReader();

            #region Plant

            string path = Path.Combine(AppConfigHelper.GetWorkspace(), "PlantSeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile(fileStream, "PlantSeedData.txt", "Plant");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Plant plant = (Plant)node;
                    if (plant.Cultivation != null)
                    {
                        manager.Create(plant.Cultivation);
                    }

                    manager.Create(plant);
                }
            }

            loadTestPlantData();

            #endregion

            #region Animal

            path = Path.Combine(AppConfigHelper.GetWorkspace(), "AnimalSeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile(fileStream, "AnimalSeedData.txt", "Animal");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Animal animal = (Animal)node;
                    manager.Create(animal);
                }
            }

            loadTestAnimalData();

            #endregion


            return View("Index");
        }

        private void loadTestPlantData()
        {
            SubjectManager manager = new SubjectManager();
            Plant tempPlant = new Plant();
            // create some big test data for plant
            for (int i = 0; i < 5; i++)
            {
                tempPlant = new Plant();
                tempPlant.Name = "Plant Name" + i;
                tempPlant.Description = "Description Description Description Description Description Description" + i;
                tempPlant.ScientificName = "ScientificName" + i;
                manager.Create(tempPlant);
            }
        }

        private void loadTestAnimalData()
        {
            SubjectManager manager = new SubjectManager();
            Animal tempAnimal = new Animal();
            // create some big test data for plant
            for (int i = 0; i < 3; i++)
            {
                tempAnimal = new Animal();
                tempAnimal.Name = "Animal Name" + i;
                tempAnimal.Description = "Description Description Description Description Description Description" + i;
                tempAnimal.ScientificName = "ScientificName" + i;
                manager.Create(tempAnimal);
            }
        }

        #endregion
    }
}