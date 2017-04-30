using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using MCT.IO;
using MCT.Search;
using MCT.Web.Helpers;
using MCT.Web.Models;
using MCT.Web.Models.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            DataModel model = new DataModel();

            SubjectManager manager = new SubjectManager();

            //model.Species = DataModelHelper.ToDataTable<Species>(manager.GetAll<Species>());

            List<PlantModel> lPLants = new List<PlantModel>();
            manager.GetAll<Plant>().ToList().ForEach(p => lPLants.Add(PlantModel.Convert(p)));
            model.Plants = DataModelHelper.ToDataTable<PlantModel>(lPLants);
            return View(model);
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

                List<Node> nodes = reader.ReadFile<Node>(fileStream, "PlantSeedData.txt", "Plant");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Plant plant = (Plant)node;
                    if (!manager.GetAll<Plant>().Any(p => p.Name.Equals(plant.Name)))
                    {
                        if (plant.Cultivation != null)
                        {
                            manager.Create(plant.Cultivation);
                        }
                    }

                    manager.Create(plant);

                }

                Debug.WriteLine("PlantSeedData.txt  : " + nodes.Count);
            }

            //mischkulturtabelle

            path = Path.Combine(AppConfigHelper.GetWorkspace(), "MischkulturTabelle.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile<Node>(fileStream, "MischkulturTabelle.txt", "Plant_MKT");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Plant plant = (Plant)node;

                    if (!string.IsNullOrEmpty(plant.Name) && !string.IsNullOrEmpty(plant.ScientificName))
                    {
                        // pflanze noch nicht vorhanden
                        if (!manager.GetAll<Plant>().Any(p => p.Name.ToLower().Equals(plant.Name.ToLower())))
                        {
                            if (plant.Cultivation != null)
                            {
                                manager.Create(plant.Cultivation);
                            }

                            manager.Create(plant);
                        }

                    }

                }

                Debug.WriteLine("MischkulturTabelle.txt  : " + nodes.Count);
            }

            //update after creation for associations

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile<Node>(fileStream, "MischkulturTabelle.txt", "Plant_MKT_UPDATE");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Plant plant = (Plant)node;
                    manager.Update(plant);
                }

                Debug.WriteLine("MischkulturTabelle.txt Update  : " + nodes.Count);
            }


            //loadTestPlantData();

            #endregion

            #region Animal

            path = Path.Combine(AppConfigHelper.GetWorkspace(), "AnimalSeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile<Node>(fileStream, "AnimalSeedData.txt", "Animal");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Animal animal = (Animal)node;
                    if (!manager.GetAll<Animal>().Any(p => p.Name.Equals(animal.Name)))
                    {
                        manager.Create(animal);
                    }
                }

                Debug.WriteLine("AnimalSeedData.txt  : " + nodes.Count);
            }

            //loadTestAnimalData();

            #endregion

            #region Effect

            path = Path.Combine(AppConfigHelper.GetWorkspace(), "EffectSeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile<Node>(fileStream, "EffectSeedData.txt", "Effect");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {

                    Effect effect = (Effect)node;
                    if (!manager.GetAll<Effect>().Any(p => p.Name.Equals(effect.Name)))
                    {
                        manager.Create(effect);
                    }
                }

                Debug.WriteLine("EffectSeedData.txt  : " + nodes.Count);
            }

            #endregion

            #region Predicate

            path = Path.Combine(AppConfigHelper.GetWorkspace(), "PredicateSeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Predicate> predicates = reader.ReadFile<Predicate>(fileStream, "PredicateSeedData.txt", "Predicate");

                SubjectManager manager = new SubjectManager();

                foreach (var node in predicates)
                {
                    Predicate predicate = (Predicate)node;
                    if (!manager.GetAll<Predicate>().Any(p => p.Name.Equals(predicate.Name)))
                    {
                        manager.Create(predicate);
                    }
                }

                Debug.WriteLine("PredicateSeedData.txt  : " + predicates.Count);
            }

            #endregion

            #region Interaction

            path = Path.Combine(AppConfigHelper.GetWorkspace(), "InteractionSeedData.txt");
            if (DataReader.FileExist(path))
            {
                SubjectManager manager = new SubjectManager();

                Stream fileStream = reader.Open(path);
                List<string> interactionsAsStringList = reader.ReadFile(fileStream, "InteractionSeedData.txt");

                List<Interaction> interactions = reader.ConvertToInteractions(interactionsAsStringList,
                    manager.GetAll<Subject>().ToList(), manager.GetAll<Predicate>().ToList());

                foreach (var node in interactions)
                {
                    if (!manager.GetAll<Interaction>().Any(i => i.Subject.Equals(node.Subject) && i.Object.Equals(node.Object)))
                    {
                        manager.Create(node);
                    }
                }

                Debug.WriteLine("InteractionSeedData.txt  : " + interactions.Count);
            }


            path = Path.Combine(AppConfigHelper.GetWorkspace(), "MischkulturTabelle.txt");

            if (DataReader.FileExist(path))
            {
                SubjectManager manager = new SubjectManager();
                Stream fileStream = reader.Open(path);
                // hoier werden alle interactions und fehlende objecte erzeugt
                List<Interaction> l = reader.ReadFile<Interaction>(fileStream, "MischkulturTabelle.txt", "Plant_MKT_UPDATE_INTERACTION");
                foreach (var node in l)
                {
                    manager.Create(node);
                }

            }

            #endregion


            return View("Index");
        }

        private void loadTestPlantData()
        {
            SubjectManager manager = new SubjectManager();
            Plant tempPlant = new Plant();
            // create some big test data for plant
            for (int i = 0; i < 1000; i++)
            {
                tempPlant = new Plant();
                tempPlant.Name = "Plant Name" + i;
                tempPlant.Description = "Description Description Description Description Description Description" + i;
                tempPlant.ScientificName = "ScientificName" + i;
                manager.Create(tempPlant);
                Debug.WriteLine(tempPlant.Name);
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