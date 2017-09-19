using MCT.Cal;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Helpers;
using MCT.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class SubjectController : Controller
    {
        public static string ALL_SUBJECTS = "ALL_SUBJECTS";
        public static string ALL_SCIENTIFIC_NAMES = "ALL_SCIENTIFIC_NAMES";
        public static string ALL_PREDICATES = "ALL_PREDICATES";

        #region Show Data

        public ActionResult Details(long id, string type)
        {
            SubjectManager sm = new SubjectManager();

            Subject s = sm.Get(id);

            //load by loading the page and store it in a session!!!!

            switch (type)
            {
                case "Plant":
                    {

                        Plant plant = sm.GetAll<Plant>().Where(p => p.Id.Equals(id)).FirstOrDefault();

                        PlantModel Model = PlantModel.Convert(plant);
                        //load interactions
                        Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(plant, true).ToList());

                        return View("PlantDetails", Model);
                    }
                case "Animal":
                    {
                        Animal animal = sm.GetAll<Animal>().Where(a => a.Id.Equals(id)).FirstOrDefault();

                        AnimalModel Model = AnimalModel.Convert(animal);
                        Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(animal, true).ToList());

                        return View("AnimalDetails", Model);
                    }
                case "Taxon":
                    {
                        Taxon taxon = sm.GetAll<Taxon>().Where(a => a.Id.Equals(id)).FirstOrDefault();
                        SubjectModel Model = SubjectModel.Convert(taxon);

                        return View("TaxonDetails", Model);
                    }
                case "Effect":
                    {
                        Effect effect = sm.GetAll<Effect>().Where(e => e.Id.Equals(id)).FirstOrDefault();

                        return View("EffectDetails");
                    }
                case "Unknow":
                    {
                        SubjectModel Model = SubjectModel.Convert(s);

                        return View("SubjectDetails", Model);
                    }
                default: { break; }
            }

            return RedirectToAction("Search", "Search"); ;
        }

        #region Gantt

        public JsonResult GetEventsForGantt(long id)
        {
            SubjectManager subjectmanager = new SubjectManager();

            //ToDo replace getAll with get plant by id
            var plant = subjectmanager.GetAll<Plant>().Where(p => p.Id.Equals(id)).FirstOrDefault();
            var events = new List<object>();

            if (plant != null)
            {
                if (plant.Bloom.Any())
                    events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Bloom));
                if (plant.Sowing.Any())
                    events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Sowing));
                if (plant.Harvest.Any())
                    events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Harvest));
                if (plant.SeedMaturity.Any())
                    events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.SeedMaturity));
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object createEventForEachPlantsTimeperiodType(Plant plant, TimePeriodType type)
        {
            var tps = new List<object>();

            switch (type)
            {
                case TimePeriodType.Bloom:
                    {
                        foreach (var VARIABLE in plant.Bloom)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }

                case TimePeriodType.Harvest:
                    {
                        foreach (var VARIABLE in plant.Harvest)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                case TimePeriodType.Sowing:
                    {
                        foreach (var VARIABLE in plant.Sowing)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                case TimePeriodType.SeedMaturity:
                    {
                        foreach (var VARIABLE in plant.SeedMaturity)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                default:
                    break;
            }


            var json = new
            {
                name = plant.Name,
                desc = type.ToString(),
                values = tps
            };

            return json;
        }

        private object getEventFromTimeperiodForGantt(TimePeriod tp, TimePeriodType type)
        {
            string color = "";

            Debug.WriteLine(tp);

            switch (type)
            {
                case TimePeriodType.Sowing: { color = "Green"; break; }
                case TimePeriodType.Harvest: { color = "Blue"; break; }
                case TimePeriodType.Bloom: { color = "Orange"; break; }
                case TimePeriodType.SeedMaturity: { color = "Red"; break; }
            }
            //ToDo datetime is not focusing on the on voll, anfang, end
            var fromDT = TimeConverter.GetStartDateTime((int)tp.StartMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
            var toDT = TimeConverter.GetEndDateTime((int)tp.EndMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));

            //name: "Testing",
            //    desc: " ",
            //    values: [{
            //    from: today_friendly,
            //        to: next_friendly,
            //        label: "Test",
            //        customClass: "ganttRed"
            //    }]


            var tpJSON = new
            {
                label = type.ToString(),
                from = "/Date(" + fromDT + ")/",
                to = "/Date(" + toDT + ")/",
                customClass = "gantt" + color
            };

            Debug.WriteLine(tpJSON);

            if (tpJSON != null)
                return tpJSON;

            return null;
        }

        #endregion Gantt

        #endregion

        #region Edit Data

        public ActionResult Edit(long id, string type)
        {
            SubjectManager sm = new SubjectManager();

            Subject s = sm.Get(id);

            //load by loading the page and store it in a session!!!!

            switch (type)
            {
                case "Plant":
                    {

                        Plant plant = sm.GetAll<Plant>().Where(p => p.Id.Equals(id)).FirstOrDefault();

                        PlantModel Model = PlantModel.Convert(plant);
                        //load interactions
                        Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(plant, true).ToList());

                        return View("PlantEdit", Model);
                    }
                case "Animal":
                    {
                        Animal animal = sm.GetAll<Animal>().Where(a => a.Id.Equals(id)).FirstOrDefault();

                        AnimalModel Model = AnimalModel.Convert(animal);
                        Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(animal, true).ToList());

                        return View("AnimalEdit", Model);
                    }
                case "Taxon":
                    {
                        Taxon taxon = sm.GetAll<Taxon>().Where(a => a.Id.Equals(id)).FirstOrDefault();
                        SubjectModel Model = SubjectModel.Convert(taxon);

                        return View("TaxonDetails", Model);
                    }
                case "Effect":
                    {
                        Effect effect = sm.GetAll<Effect>().Where(e => e.Id.Equals(id)).FirstOrDefault();

                        return View("EffectDetails");
                    }
                case "Unknow":
                    {
                        SubjectModel Model = SubjectModel.Convert(s);

                        return View("SubjectDetails", Model);
                    }
                default: { break; }
            }

            return RedirectToAction("Search", "Search");
        }

        public ActionResult CreatePlant()
        {
            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus

            return View("PlantEdit", new PlantModel());
        }

        public ActionResult CreateAnimal()
        {
            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus

            return View("AnimalEdit", new AnimalModel());
        }


        //ToDO find a way to handle a form in a other way :D
        /// <summary>
        /// only a action to return from ajax form for validation
        /// </summary>
        /// <returns></returns>
        public ActionResult X()
        {
            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus

            return Json("x", JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteNode(long id)
        {
            try
            {
                SubjectManager subjectManager = new SubjectManager();
                //InteractionManager interactionManager = new InteractionManager();
                Node node = subjectManager.GetAllAsQueryable<Node>().Where(n => n.Id.Equals(Convert.ToInt64(id))).FirstOrDefault();

                //remove all dependend interacions
                //var interactions = subjectManager.GetAllDependingInteractions(node).ToList();

                //for (int i = 0; i < interactions.Count(); i++)
                //{
                //    interactionManager.Delete(interactions[i]);
                //}

                subjectManager.DeleteNode(node);

                return Json(true);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult SavePlant(Plant plant)
        {
            try
            {
                //Todo Select the type based on the scientific name
                plant.Rank = Utility.GetTaxonRank(plant.ScientificName);


                //TODO Generate the Parent based on the ScientificName
                // a a a = SubSpecies, a a = Species, a = Genus
                // a a var. a is also a species
                /* - based on the scientficname create or set the parents
             * - use maybe some webservices to create missing one
             *
             */
                SubjectManager subjectManager = new SubjectManager();
                InteractionManager interactionManager = new InteractionManager();

                plant.Parent = Utility.CreateOrSetParents(plant.ScientificName, typeof(Plant), subjectManager);

                //ToDO check all entities that comes from the ui that has no id. they need to get from or create
                /* all timeperiods need the have the id from the created plant
             * - need to create the plant frist??
             * - maybe task fro the udatemanager
             * */


                //ToDo Store Image in folder : project/images/
                //add a media to plant

                //corecction the timperiod type
                //ToDo remove ugly preperations

                #region ugly preperations

                if (plant.Sowing.Any())
                {
                    foreach (var item in plant.Sowing)
                    {
                        item.Type = TimePeriodType.Sowing;
                    }
                }

                if (plant.Harvest.Any())
                {
                    foreach (var item in plant.Harvest)
                    {
                        item.Type = TimePeriodType.Harvest;
                    }
                }

                if (plant.SeedMaturity.Any())
                {
                    foreach (var item in plant.SeedMaturity)
                    {
                        item.Type = TimePeriodType.SeedMaturity;
                    }
                }

                #endregion

                if (plant.Id == 0)
                    plant = subjectManager.CreatePlant(plant);
                else
                {
                    subjectManager.Update(plant);
                }

                return Json(plant.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult SaveAnimal(Animal animal)
        {
            SubjectManager subjectManager = new SubjectManager();
            InteractionManager interactionManager = new InteractionManager();

            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus
            // a a var. a is also a species
            /* - based on the scientficname create or set the parents
             * - use maybe some webservices to create missing one
             *
             */

            //Todo Select the type based on the scientific name
            animal.Rank = Utility.GetTaxonRank(animal.ScientificName);

            animal.Parent = Utility.CreateOrSetParents(animal.ScientificName, typeof(Animal), subjectManager);

            //ToDO check all entities that comes from the ui that has no id. they need to get from or create
            /* all timeperiods need the have the id from the created plant
             * - need to create the plant frist??
             * - maybe task fro the udatemanager
             * */


            //ToDo Store Image in folder : project/images/
            //add a media to plant

            if (animal.Id == 0)
                animal = subjectManager.CreateAnimal(animal);
            else
            {
                subjectManager.Update(animal);
            }


            return Json(animal.Id, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveImage(long id)
        {

            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(fileContent.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);

                            SubjectManager subjectManager = new SubjectManager();
                            Subject s = subjectManager.Get(id);

                            Media media = new Media();
                            media.ImagePath = "/Images/" + fileName;

                            s.Medias.Add(media);

                            subjectManager.Update(s);

                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed", JsonRequestBehavior.AllowGet);
            }

            return Json("File uploaded successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSubjects()
        {
            return Json(getAllSubjectNames(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllScientificNamesResult()
        {
            return Json(getAllScientficNames(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllPredicates()
        {
            return Json(getAllPredicateNames(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllNames()
        {
            return Json(getAllNames(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmptyTimePeriod()
        {
            // type of the model dont matter.
            // its important that its a entity from timeperiod
            return PartialView("TimePeriod", new Bloom());
        }

        public ActionResult GetEmptySimpleLink()
        {
            return PartialView("SimpleLinkModel", new SimpleLinkModel());
        }



        /// <summary>
        /// Check if Name exist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckNameExist(string name, string initName)
        {
            SubjectManager subjectManager = new SubjectManager();

            string defaultstring = "";
            if (!String.IsNullOrEmpty(initName)) defaultstring = initName;

            if (subjectManager.GetAll<Node>().Any(n => n.Name != null && n.Name.Equals(name) && !n.Name.Equals(defaultstring))) return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check if Name exist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckScientificNameExist(string scientificName, string initScientificName)
        {
            SubjectManager subjectManager = new SubjectManager();
            string defaultstring = "";
            if (!String.IsNullOrEmpty(initScientificName)) defaultstring = initScientificName;


            if (subjectManager.GetAll<Node>().Any(n => n.ScientificName != null && n.ScientificName.Equals(scientificName) && !n.ScientificName.Equals(defaultstring))) return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check if Name exist, returns true if exist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckNameOfSimpleLink(string name)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Node>().Any(n => n.Name.Equals(name))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Session

        private List<string> getAllSubjectNames()
        {
            if (Session[ALL_SUBJECTS] == null)
            {
                SubjectManager subjectManager = new SubjectManager();

                //ToDo change the position of load this
                //load Viewdata
                // load all subject for autocomplete
                Session[ALL_SUBJECTS] = subjectManager.GetAll<Subject>().Select(s => s.Name).ToList();
                //Session[ALL_PREDICATES] = subjectManager.GetAll<Predicate>().Select(s => s.Name);
            }

            List<string> tmp = Session[ALL_SUBJECTS] as List<string>;
            tmp.Sort();
            return tmp;
        }

        private List<string> getAllScientficNames()
        {
            if (Session[ALL_SCIENTIFIC_NAMES] == null)
            {
                SubjectManager subjectManager = new SubjectManager();

                //ToDo change the position of load this
                //load Viewdata
                // load all subject for autocomplete
                Session[ALL_SCIENTIFIC_NAMES] = subjectManager.GetAll<Species>().Select(s => s.ScientificName).ToList();
                //Session[ALL_PREDICATES] = subjectManager.GetAll<Predicate>().Select(s => s.Name);
            }

            List<string> tmp = Session[ALL_SCIENTIFIC_NAMES] as List<string>;
            tmp.Sort();
            return tmp;
        }

        private List<string> getAllPredicateNames()
        {
            if (Session[ALL_PREDICATES] == null)
            {
                SubjectManager subjectManager = new SubjectManager();

                //ToDo change the position of load this
                //load Viewdata
                // load all subject for autocomplete
                Session[ALL_PREDICATES] = subjectManager.GetAll<Predicate>().Select(s => s.Name);
                //Session[ALL_PREDICATES] = subjectManager.GetAll<Predicate>().Select(s => s.Name);
            }

            List<string> tmp = Session[ALL_PREDICATES] as List<string>;

            return tmp;

        }

        private List<string> getAllNames()
        {
            List<string> tmp = getAllSubjectNames();
            if (getAllScientficNames() != null && getAllScientficNames().Any())
                tmp = tmp.Concat(getAllScientficNames()).ToList();
            if (getAllPredicateNames() != null && getAllPredicateNames().Any())
                tmp = tmp.Concat(getAllPredicateNames()).ToList();

            tmp.Sort();
            return tmp;
        }

        #endregion
    }
}