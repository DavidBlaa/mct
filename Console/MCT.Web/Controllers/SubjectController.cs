using MCT.Cal;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Extern;
using MCT.Web.Helpers;
using MCT.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
                        NodeModel Model = NodeModel.Convert(taxon);

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
            var subject = subjectmanager.Get(id);
            var events = new List<object>();

            if (subject != null)
            {
                events.AddRange(GantHelper.GetAllEventsFromSubject(subject));
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        #endregion Gantt

        #endregion Show Data

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

                        //load ParentList
                        ViewData["Parent.Name"] = getAllNodeNames(Model.TaxonRank);

                        return View("PlantEdit", Model);
                    }
                case "Animal":
                    {
                        Animal animal = sm.GetAll<Animal>().Where(a => a.Id.Equals(id)).FirstOrDefault();

                        AnimalModel Model = AnimalModel.Convert(animal);
                        Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(animal, true).ToList());

                        //load ParentList
                        ViewData["Parent.Name"] = getAllNodeNames(Model.TaxonRank);

                        return View("AnimalEdit", Model);
                    }
                case "Taxon":
                    {
                        Taxon taxon = sm.GetAll<Taxon>().Where(a => a.Id.Equals(id)).FirstOrDefault();
                        NodeModel Model = NodeModel.Convert(taxon);

                        //load ParentList
                        ViewData["Parent.Name"] = getAllNodeNames(Model.TaxonRank);

                        return View("TaxonEdit", Model);
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

            ViewData["Parent.Name"] = getAllNodeNames(TaxonRank.SubSpecies);

            return View("PlantEdit", new PlantModel());
        }

        public ActionResult CreateAnimal()
        {
            ViewData["Parent.Name"] = getAllNodeNames(TaxonRank.SubSpecies);

            return View("AnimalEdit", new AnimalModel());
        }

        public ActionResult CreateTaxon()
        {
            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus

            return View("TaxonEdit", new NodeModel());
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

        public ActionResult SavePlant(PlantModel plantModel)
        {
            try
            {
                SubjectManager subjectManager = new SubjectManager();
                InteractionManager interactionManager = new InteractionManager();

                Plant plant = new Plant();
                if (plantModel.Id > 0)
                {
                    var x = subjectManager.Get(plantModel.Id).Self;
                    plant = x as Plant;
                }

                plant.Name = plantModel.Name;
                plant.ScientificName = plantModel.ScientificName;

                //Todo Select the type based on the scientific name
                //plant.Rank = Utility.GetTaxonRank(plantModel.ScientificName);
                plant.Rank = plantModel.TaxonRank;

                #region after culture

                foreach (var ac in plantModel.AfterCultures)
                {
                    if (string.IsNullOrEmpty(ac.Name)) break;

                    Plant afterCultureTmp = null;

                    //add new culture to plant
                    if (ac.Id == 0)
                    {
                        afterCultureTmp = subjectManager.GetAllAsQueryable<Plant>().Where(p => p.Name.Equals(ac.Name)).FirstOrDefault();
                        if (afterCultureTmp != null) plant.AfterCultures.Add(subjectManager.Get(afterCultureTmp.Id) as Plant);
                    }
                }

                //delete cultures
                List<string> nameOfDeleteAfterCultures = new List<string>();
                foreach (var ac in plant.AfterCultures)
                {
                    if (!plantModel.AfterCultures.Any(a => a.Name.Equals(ac.Name)))
                        nameOfDeleteAfterCultures.Add(ac.Name);
                }

                foreach (var name in nameOfDeleteAfterCultures)
                {
                    var tmp = plant.AfterCultures.Where(a => a.Name.Equals(name)).FirstOrDefault();
                    if (tmp != null) plant.AfterCultures.Remove(tmp);
                }

                #endregion after culture

                #region preculture

                foreach (var pc in plantModel.PreCultures)
                {
                    if (string.IsNullOrEmpty(pc.Name)) break;

                    Plant preCultureTmp = null; ;

                    //add new culture to plant
                    if (pc.Id == 0)
                    {
                        preCultureTmp = subjectManager.GetAllAsQueryable<Plant>().Where(p => p.Name.Equals(pc.Name)).FirstOrDefault();
                        if (preCultureTmp != null) plant.PreCultures.Add(subjectManager.Get(preCultureTmp.Id) as Plant);
                    }
                }

                //delete cultures
                List<string> nameOfDeletePreCultures = new List<string>();
                foreach (var ac in plant.PreCultures)
                {
                    if (!plantModel.PreCultures.Any(a => a.Name.Equals(ac.Name)))
                        nameOfDeletePreCultures.Add(ac.Name);
                }

                foreach (var name in nameOfDeletePreCultures)
                {
                    var tmp = plant.PreCultures.Where(a => a.Name.Equals(name)).FirstOrDefault();
                    if (tmp != null) plant.PreCultures.Remove(tmp);
                }

                #endregion preculture

                plant.Description = plantModel.Description;
                plant.Height = plantModel.Height;
                plant.LocationType = plantModel.LocationType;

                if (!plantModel.ImagePath.Equals("/Images/Empty.png") && !plant.Medias.Where(m => m.ImagePath.Equals(plantModel.ImagePath)).Any())
 
                    plant.Medias.Add(new Media()
                    {
                        ImagePath = plantModel.ImagePath,
                        MIMEType = MimeMapping.GetMimeMapping(plantModel.ImagePath)
                    });

                plant.NutrientClaim = plantModel.NutrientClaim;
                plant.RootDepth = plantModel.RootDepth;
                plant.SowingDepth = plantModel.SowingDepth;
                plant.Width = plantModel.Width;

                plant.TimePeriods = new List<TimePeriod>();

                foreach (var lifeCylce in plantModel.LifeCycles)
                {
                    lifeCylce.Reverse();
                    TimePeriod last = null;
                    foreach (var tpModel in lifeCylce)
                    {
                        TimePeriod tp = Utility.CreateTimePeriodFromModel(tpModel);
                        tp.AssignedTo = plant;

                        if (lifeCylce.Last().Equals(tpModel)) tp.Start = true;
                        if (last != null) tp.Next = last;

                        plant.TimePeriods.Add(tp);

                        last = tp;
                    }

                    plant.TimePeriods.Reverse();
                }

                if (plantModel.Id == 0)
                {
                    plant = subjectManager.CreatePlant(plant);
                }
                else
                {
                    plant = subjectManager.UpdatePlant(plant);
                }

                //set parent
                if (plantModel.Parent != null && plantModel.Parent.Id > 0)
                {
                    var parent = subjectManager.GetAll<Node>().Where(n => n.Id.Equals(plantModel.Parent.Id)).FirstOrDefault();
                    plant.Parent = parent;
                }

                subjectManager.Update(plant);

                return Json(plant.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private Node LoadParent(Node node, SubjectManager subjectManager)
        {
            if (node.Parent != null) LoadParent(node.Parent, subjectManager);

            return subjectManager.GetAll<Node>().Where(n => n.Id.Equals(node.Id)).FirstOrDefault();
        }

        public ActionResult SaveAnimal(AnimalModel animalModel)
        {
            try
            {
                SubjectManager subjectManager = new SubjectManager();
                InteractionManager interactionManager = new InteractionManager();

                Animal animal = new Animal();
                if (animalModel.Id > 0)
                {
                    var x = subjectManager.Get(animalModel.Id).Self;
                    animal = x as Animal;
                }

                animal.Name = animalModel.Name;
                animal.ScientificName = animalModel.ScientificName;
                animal.Rank = animalModel.TaxonRank;
                //TODO Generate the Parent based on the ScientificName
                // a a a = SubSpecies, a a = Species, a = Genus
                // a a var. a is also a species
                /* - based on the scientficname create or set the parents
                 * - use maybe some webservices to create missing one
                 *
                 */

                //Todo Select the type based on the scientific name
                //animal.Rank = Utility.GetTaxonRank(animal.ScientificName);

                if (!animalModel.ImagePath.Equals("/Images/Empty.png") && !animal.Medias.Where(m => m.ImagePath.Equals(animalModel.ImagePath)).Any())
                    animal.Medias.Add(new Media()
                    {
                        ImagePath = animalModel.ImagePath,
                        MIMEType = MimeMapping.GetMimeMapping(animalModel.ImagePath)
                    });

                //lifecycles
                animal.TimePeriods = new List<TimePeriod>();
                foreach (var lifeCylce in animalModel.LifeCycles)
                {
                    lifeCylce.Reverse();
                    TimePeriod last = null;
                    foreach (var tpModel in lifeCylce)
                    {
                        TimePeriod tp = Utility.CreateTimePeriodFromModel(tpModel);
                        tp.AssignedTo = animal;

                        if (lifeCylce.Last().Equals(tpModel)) tp.Start = true;
                        if (last != null) tp.Next = last;

                        animal.TimePeriods.Add(tp);

                        last = tp;
                    }

                    animal.TimePeriods.Reverse();
                }

                if (animal.Id == 0)
                    animal = subjectManager.CreateAnimal(animal);
                else
                {
                    subjectManager.Update(animal);
                    //animal.Parent = Utility.CreateOrSetParents(animal.ScientificName, typeof(Animal), subjectManager);
                    //subjectManager.Update(animal);
                }

                //set parent
                if (animalModel.Parent != null && animalModel.Parent.Id > 0)
                {
                    var parent = subjectManager.GetAll<Node>().Where(n => n.Id.Equals(animalModel.Parent.Id)).FirstOrDefault();
                    animal.Parent = parent;

                }

                subjectManager.Update(animal);

                return Json(animal.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveTaxon(NodeModel taxonModel)
        {
            SubjectManager subjectManager = new SubjectManager();
            InteractionManager interactionManager = new InteractionManager();

            //ToDo Store Image in folder : project/images/
            //add a media to plant

            Taxon taxon = new Taxon();
            if (taxonModel.Id > 0)
            {
                var x = subjectManager.Get(taxonModel.Id).Self;
                taxon = x as Taxon;
            }

            taxon.Name = taxonModel.Name;
            taxon.ScientificName = taxonModel.ScientificName;
            taxon.Rank = taxonModel.TaxonRank;
            //TODO Generate the Parent based on the ScientificName
            // a a a = SubSpecies, a a = Species, a = Genus
            // a a var. a is also a species
            /* - based on the scientficname create or set the parents
             * - use maybe some webservices to create missing one
             *
             */

            //Todo Select the type based on the scientific name
            //animal.Rank = Utility.GetTaxonRank(animal.ScientificName);

            if (!taxon.Medias.Where(m => m.ImagePath.Equals(taxonModel.ImagePath)).Any())
                taxon.Medias.Add(new Media()
                {
                    ImagePath = taxonModel.ImagePath,
                    MIMEType = MimeMapping.GetMimeMapping(taxonModel.ImagePath)
                });


            if (taxon.Id == 0)
                taxon = subjectManager.CreateTaxon(taxon);
            else
            {
                subjectManager.Update(taxon);
            }

            //set parent
            if (taxonModel.Parent != null && taxonModel.Parent.Id > 0)
            {
                var parent = subjectManager.GetAll<Node>().Where(n=>n.Id.Equals(taxonModel.Parent.Id)).FirstOrDefault();
                taxon.Parent = parent;

            }

            subjectManager.Update(taxon);



            return Json(taxon.Id, JsonRequestBehavior.AllowGet);
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

                        if (fileName.Equals("Empty.png"))
                        {
                            return Json("no image selected", JsonRequestBehavior.AllowGet);
                        }

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
            return PartialView("TimePeriod", new TimePeriodModel());
        }

        public ActionResult GetEmptyLifeCycle()
        {
            return PartialView("TimePeriods", new List<TimePeriodModel>());
        }

        public ActionResult GetEmptySimpleLink()
        {
            return PartialView("SimpleLinkModel", new SimpleLinkModel());
        }

        public ActionResult GetEmptyCulture()
        {
            return PartialView("CultureModel", new CultureModel());
        }

        public JsonResult GetName(string scientificName)
        {
            WikipediaReader wikipediaRaeder = new WikipediaReader();
            string name = wikipediaRaeder.GetName(scientificName);

            return Json(name);
        }

        public JsonResult GetScientificName(string name)
        {
            WikipediaReader wikipediaRaeder = new WikipediaReader();
            string scientificName = wikipediaRaeder.GetScientificName(name);

            return Json(scientificName);
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

        public JsonResult GetAllNodes(TaxonRank rank)
        {
            return Json(getAllNodeNames(rank), JsonRequestBehavior.AllowGet);
        }

        #endregion Edit Data

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

        private SelectList getAllNodeNames(TaxonRank rank)
        {
            TaxonRank nextRank = Enum.GetValues(typeof(TaxonRank)).Cast<TaxonRank>()
                    .SkipWhile(e => e != rank).Skip(1).First();

            List<SelectListItem> list = new List<SelectListItem>();

            SubjectManager subjectManager = new SubjectManager();
            var nodes = subjectManager.GetAll<Node>().Where(n => n.Rank.Equals(nextRank));

            nodes.OrderBy(n=>n.Name).ToList().ForEach(n => list.Add(new SelectListItem() { Text = n.Name, Value = n.Id.ToString() }));

            return new SelectList(list, "Value", "Text");
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

        #endregion Session

        #region Helper

        private void saveLifeCycle(List<TimePeriodModel> lifeCycle, Subject subject)
        {
            TimePeriod prev = null;
            for (int i = lifeCycle.Count - 1; i > 0; i--)
            {
            }
        }

        #endregion Helper
    }
}