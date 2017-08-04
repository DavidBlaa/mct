﻿using MCT.Cal;
using MCT.DB.Entities;
using MCT.DB.Services;
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

            return Json("x");
        }

        public ActionResult DeleteSubject(long id)
        {
            try
            {
                SubjectManager subjectManager = new SubjectManager();
                Subject subject = subjectManager.Get(Convert.ToInt64(id));
                subjectManager.Delete(subject);

                return Json(true);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult SavePlant(Plant plant, List<Interaction> interactions)
        {
            try
            {
                //Todo Select the type based on the scientific name
                if (plant.ScientificName.Split(' ').Length > 1) plant.Rank = TaxonRank.SubSpecies;
                else if (plant.ScientificName.Split(' ').Length == 1) plant.Rank = TaxonRank.Species;


                //TODO Generate the Parent based on the ScientificName
                // a a a = SubSpecies, a a = Species, a = Genus
                /* - based on the scientficname create or set the parents
             * - use maybe some webservices to create missing one
             *
             */

                //ToDO check all entities that comes from the ui that has no id. they need to get from or create
                /* all timeperiods need the have the id from the created plant
             * - need to create the plant frist??
             * - maybe task fro the udatemanager
             * */
                SubjectManager subjectManager = new SubjectManager();
                InteractionManager interactionManager = new InteractionManager();

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
                    if (interactions != null)
                    {

                        //Delete interactions
                        IEnumerable<Interaction> interactionListFromDB =
                            subjectManager.GetAllDependingInteractions(plant);
                        for (int i = 0; i < interactionListFromDB.Count(); i++)
                        {
                            Interaction tmp = interactionListFromDB.ElementAt(i);
                            if (!interactions.Any(x => x.Id.Equals(tmp.Id)))
                                interactionManager.Delete(tmp);
                        }
                    }

                }

                #region save or update interactions

                saveOrUpdateInteractions(interactions);

                #endregion

                //save or update interactions

                return Json(plant.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveAnimal(Animal animal, List<Interaction> interactions = null)
        {
            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus
            /* - based on the scientficname create or set the parents
             * - use maybe some webservices to create missing one
             *
             */

            //ToDO check all entities that comes from the ui that has no id. they need to get from or create
            /* all timeperiods need the have the id from the created plant
             * - need to create the plant frist??
             * - maybe task fro the udatemanager
             * */
            SubjectManager subjectManager = new SubjectManager();
            InteractionManager interactionManager = new InteractionManager();

            //ToDo Store Image in folder : project/images/
            //add a media to plant

            if (animal.Id == 0)
                animal = subjectManager.CreateAnimal(animal);
            else
            {
                subjectManager.Update(animal);
                if (interactions != null)
                {
                    //Delete interactions
                    IEnumerable<Interaction> interactionListFromDB = subjectManager.GetAllDependingInteractions(animal);
                    for (int i = 0; i < interactionListFromDB.Count(); i++)
                    {
                        Interaction tmp = interactionListFromDB.ElementAt(i);
                        if (!interactions.Any(x => x.Id.Equals(tmp.Id)))
                            interactionManager.Delete(tmp);
                    }
                }

            }

            #region save or update interactions

            saveOrUpdateInteractions(interactions);

            #endregion
            //save or update interactions

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
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }

        //ToDo Function --> InteractionManager
        //ToD0 Error by saving interaction that exist in the system
        private void saveOrUpdateInteractions(List<Interaction> interactions)
        {
            SubjectManager subjectManager = new SubjectManager();
            InteractionManager interactionManager = new InteractionManager();

            if (interactions != null && interactions.Any())
            {

                foreach (var interaction in interactions)
                {
                    List<Subject> all = subjectManager.GetAll<Subject>().ToList();
                    List<Predicate> allPredicates = subjectManager.GetAll<Predicate>().ToList();

                    //check if all entities has a 0 id, then it needs to create first
                    if (interaction.Subject != null && interaction.Subject.Id == 0)
                    {
                        if (all.Where(s => s.Name.Equals(interaction.Subject.Name)).Any())
                        {
                            var obj = all.Where(s => s.Name.Equals(interaction.Subject.Name)).FirstOrDefault();
                            interaction.Subject = obj;
                        }
                        else
                            interaction.Subject = subjectManager.Create(interaction.Subject);
                    }

                    if (interaction.Object != null && interaction.Object.Id == 0)
                    {
                        if (all.Where(s => s.Name.Equals(interaction.Object.Name)).Any())
                        {
                            var obj = all.Where(s => s.Name.Equals(interaction.Object.Name)).FirstOrDefault();
                            interaction.Object = obj;
                        }
                        else
                            interaction.Object = subjectManager.Create(interaction.Object);

                    }

                    if (interaction.Predicate != null && interaction.Predicate.Id == 0)
                    {
                        if (allPredicates.Where(s => s.Name.Equals(interaction.Predicate.Name)).Any())
                        {
                            var obj = allPredicates.Where(s => s.Name.Equals(interaction.Predicate.Name)).FirstOrDefault();
                            interaction.Predicate = obj;
                        }
                        else
                        {
                            if (interaction.Predicate.Parent.Id == 0)
                            {
                                interaction.Predicate.Parent.Id = allPredicates.Where(p => p.Name.Equals(interaction.Predicate.Parent.Name)).FirstOrDefault().Id;
                            }
                            interaction.Predicate = subjectManager.Create(interaction.Predicate);

                        }
                    }

                    if (interaction.ImpactSubject != null && !String.IsNullOrEmpty(interaction.ImpactSubject.Name) && interaction.ImpactSubject.Id == 0)
                    {
                        if (all.Where(s => s.Name.Equals(interaction.ImpactSubject.Name)).Any())
                        {
                            var obj = all.Where(s => s.Name.Equals(interaction.ImpactSubject.Name)).FirstOrDefault();
                            interaction.ImpactSubject = obj;
                        }
                        else
                            interaction.ImpactSubject = subjectManager.Create(interaction.ImpactSubject);

                    }
                    else
                    {
                        interaction.ImpactSubject = null;
                    }

                    interactionManager.Update(interaction);
                }

            }
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

        public ActionResult GetEmptyInteraction()
        {
            return PartialView("Interaction", new InteractionModel());
        }

        /// <summary>
        /// Check if Name exist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckNameExist(string name, string initName)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Node>().Any(n => n.Name.Equals(name) && !n.Name.Equals(initName))) return Json(false, JsonRequestBehavior.AllowGet);

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

            if (subjectManager.GetAll<Node>().Any(n => n.ScientificName.Equals(scientificName) && !n.ScientificName.Equals(initScientificName))) return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
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