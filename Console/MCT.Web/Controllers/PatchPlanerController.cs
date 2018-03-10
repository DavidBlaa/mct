using MCT.DB.Entities;
using MCT.DB.Entities.PatchPlaner;
using MCT.DB.Services;
using MCT.Search;
using MCT.Web.Helpers.PatchPlaner;
using MCT.Web.Models;
using MCT.Web.Models.PatchPlaner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class PatchPlanerController : Controller
    {
        // GET: Patch
        public ActionResult Index(long id, bool reset = true)
        {
            PatchPlanerModel model = new PatchPlanerModel();

            #region create patch Model
            //get patch from db;
            PatchManager patchManager = new PatchManager();
            Patch p = patchManager.Get(id);
            PatchModel patchModel = PatchModelHelper.ConvertTo(p);

            #endregion

            #region create model for search

            getAllNames();

            if (reset) ResetSearchProvider();

            SearchProvider sp = GetSearchProvider();
            SubjectManager subjectManager = new SubjectManager();

            SearchModel searchModel = new SearchModel();
            SearchManager searchManager = new SearchManager();

            var plants = searchManager.SearchPlants(sp.SearchCriterias);
            if (plants != null)
            {
                //convert all subjects to subjectModels
                plants = plants.OrderBy(s => s.Name);
                plants.ToList().ForEach(s => searchModel.Plants.Add(PlantModel.Convert(s)));
            }

            searchModel.SearchCriterias = sp.SearchCriterias;

            #endregion

            model.Patch = patchModel;
            model.Search = searchModel;

            return View(model);
        }

        public ActionResult Patch(long id)
        {
            //get patch from db;
            PatchManager patchManager = new PatchManager();
            Patch p = patchManager.Get(id);

            PatchModel model = PatchModelHelper.ConvertTo(p);


            return View(model);
        }

        public ActionResult AddRandomPlant(long id)
        {
            PatchManager patchManager = new PatchManager();
            Patch patch = patchManager.Get(id);
            SubjectManager subjectManager = new SubjectManager();

            List<long> ids = subjectManager.GetAll<Plant>().Select(pa => pa.Id).ToList();


            Random random = new Random();
            int randomInt = random.Next(1, ids.Count - 1);

            long plantId = ids.ElementAt(randomInt);

            Plant p = subjectManager.Get(plantId) as Plant;

            Placement placement = new Placement();
            placement.Plant = p;
            placement.Patch = patch;

            patch.PatchElements.Add(placement);

            patchManager.Update(patch);

            PlacementModel model = PatchModelHelper.ConvertTo(placement);

            return PartialView("Placement", model);
        }

        public ActionResult AddPlant(long id, long patchId)
        {

            try
            {
                PatchManager patchManager = new PatchManager();
                Patch patch = patchManager.Get(patchId);
               
                SubjectManager subjectManager = new SubjectManager();

                long plantId = id;

                Plant p = subjectManager.GetAll<Plant>().Where(x=>x.Id.Equals(plantId)).FirstOrDefault();

                Placement placement = new Placement();
                placement.Plant = p;
                placement.Patch = patch;

                PlacementModel model = PatchModelHelper.ConvertTo(placement);

                return PartialView("Placement", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// save patch and all placemenets
        /// 
        /// need by the placemenet
        /// id, transformation, patchref, plantref,planting month, planting area
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(List<PlacementJsonModel> placements)
        {
            long patchId = placements.First().PatchId;

            PatchManager patchManager = new PatchManager();
            Patch patch = patchManager.Get(patchId);


            //delete
            var x = new List<PatchElement>();
            foreach (var pe in patch.PatchElements)
            {
                if (!placements.Any(p => p.Id.Equals(pe.Id))) x.Add(pe);
            }

            foreach (var e in x)
            {
                patch.PatchElements.Remove(e);
            }


            // add or update
            foreach (var placementJson in placements)
            {
                if (placementJson.Id == 0)
                {
                    patch.PatchElements.Add(PatchModelHelper.ConvertTo(placementJson));
                }
                else
                {
                    var pe = patch.PatchElements.FirstOrDefault(p => p.Id.Equals(placementJson.Id));
                    pe.Transformation = placementJson.Transformation;
                }
            }

            patchManager.Update(patch);

            //check deleted

            return Json(true);
        }

        //public JsonResult RemovePlacement(long id, long patchId)
        //{

        //    try
        //    {
        //        PatchManager patchManager = new PatchManager();
        //        Patch patch = patchManager.Get(patchId);

        //        PatchElement pe = patch.PatchElements.Where(p => p.Id.Equals(id)).FirstOrDefault();
        //        patch.PatchElements.Remove(pe);

        //        patchManager.Update(patch);

        //        return Json(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region search

        public static string ALL_SUBJECTS = "ALL_SUBJECTS";
        public static string ALL_SCIENTIFIC_NAMES = "ALL_SCIENTIFIC_NAMES";
        public static string ALL_PREDICATES = "ALL_PREDICATES";

        // GET: Search
        [HttpGet]
        public ActionResult Search(bool reset = false)
        {
            //ToDo think about setting sessions, is this the rigth place ?
            //load all names in session

            getAllNames();

            if (reset) ResetSearchProvider();

            SearchProvider sp = GetSearchProvider();
            SubjectManager subjectManager = new SubjectManager();

            //Get all subjects
            var subjects = subjectManager.GetAll<Subject>();

            SearchModel Model = new SearchModel();

            SearchManager searchManager = new SearchManager();

            IEnumerable<Plant> plants = searchManager.SearchPlants(sp.SearchCriterias).ToList();
            if (plants != null)
            {
                //convert all subjects to subjectModels
                plants = plants.OrderBy(p => p.Name);
                plants.ToList().ForEach(s => Model.Plants.Add(PlantModel.Convert(s)));
            }

            Model.SearchCriterias = sp.SearchCriterias;


            return View("Search", Model);
        }

        // GET: Search
        [HttpPost]
        public ActionResult Search(string searchValue)
        {
            SearchProvider sp = GetSearchProvider();

            if (searchValue == null)
                sp.DeleteSearchCriterias(SearchProvider.FREETEXT_SEARCH_KEY);
            else
                sp.UpateSearchCriterias(SearchProvider.FREETEXT_SEARCH_KEY, searchValue);

            Debug.WriteLine("SEARCH : " + searchValue);

            List<PlantModel> Model = new List<PlantModel>();
            SubjectManager subjectManager = new SubjectManager();

            //Get filtered subjects
            //var species = sp.Search();

            SearchManager searchManager = new SearchManager();

            var plants = searchManager.SearchPlants(sp.SearchCriterias);
            if (plants != null)
            {
                //convert all subjects to subjectModels
                plants = plants.OrderBy(p => p.Name);
                plants.ToList().ForEach(s => Model.Add(PlantModel.Convert(s)));
            }

            //update searchcriterias

            return PartialView("_searchResult", Model);

        }

        public JsonResult SetFilter(string key, string value)
        {
            SearchProvider sp = GetSearchProvider();

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                sp.UpateSearchCriterias(key, value);
            else
                sp.DeleteSearchCriterias(key);

            return Json(true);
        }

        public ActionResult UpdateSearch()
        {
            List<NodeModel> Model = new List<NodeModel>();
            SearchProvider sp = GetSearchProvider();
            SearchManager searchManager = new SearchManager();

            var species = searchManager.Search(sp.SearchCriterias);

            if (species != null)
            {
                //convert all subjects to subjectModels
                species = species.OrderBy(p => p.Name);
                species.ToList().ForEach(s => Model.Add(NodeModel.Convert(s)));
            }

            return PartialView("_searchResult", Model);
        }

        #region Breadcrumb

        public ActionResult UpdateBreadcrumb()
        {
            return PartialView("_searchBreadcrumb", GetSearchProvider().SearchCriterias);
        }

        public JsonResult DeleteSearchCriteria(string key)
        {
            SearchProvider sp = GetSearchProvider();

            if (!string.IsNullOrEmpty(key))
                sp.DeleteSearchCriterias(key);

            return Json(true);
        }

        #endregion

        #region Helpers

        public ActionResult GetAllNames()
        {
            return Json(getAllNames(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Sessions

        private SearchProvider GetSearchProvider()
        {
            if (Session[SearchProvider.SEARCH_PROVIDER_NAME] == null)
            {
                Session[SearchProvider.SEARCH_PROVIDER_NAME] = new SearchProvider();

            }

            return Session[SearchProvider.SEARCH_PROVIDER_NAME] as SearchProvider;
        }

        private SearchProvider ResetSearchProvider()
        {
            Session[SearchProvider.SEARCH_PROVIDER_NAME] = new SearchProvider();
            return Session[SearchProvider.SEARCH_PROVIDER_NAME] as SearchProvider;
        }

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


        #endregion

    }
}