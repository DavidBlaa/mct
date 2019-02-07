using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Search;
using MCT.Web.Helpers;
using MCT.Web.Models;
using MCT.Web.Models.Search;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class SearchController : Controller
    {
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

            SearchModel Model = new SearchModel(subjects.ToList().OrderBy(s => s.Name).ToList());

            SearchManager searchManager = new SearchManager();

            var species = searchManager.Search(sp.SearchCriterias);
            if (species != null)
            {
                //convert all subjects to subjectModels
                species = species.OrderBy(p => p.Name);
                species.ToList().ForEach(s => Model.Species.Add(NodeModel.Convert(s)));
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

            List<NodeModel> Model = new List<NodeModel>();
            SubjectManager subjectManager = new SubjectManager();

            //Get filtered subjects
            //var species = sp.Search();

            SearchManager searchManager = new SearchManager();

            var species = searchManager.Search(sp.SearchCriterias);
            if (species != null)
            {
                //convert all subjects to subjectModels
                species = species.OrderBy(p => p.Name);
                species.ToList().ForEach(s => Model.Add(NodeModel.Convert(s)));
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
            List<BreadcrumbModel> model = new List<BreadcrumbModel>();

            foreach (var kvp in GetSearchProvider().SearchCriterias)
            {
                model.Add(ModelHelper.ConvertTo(kvp));
            }

            return PartialView("_searchBreadcrumb", model);
        }

        public JsonResult DeleteSearchCriteria(string key)
        {
            SearchProvider sp = GetSearchProvider();

            if (!string.IsNullOrEmpty(key))
                sp.DeleteSearchCriterias(key);

            return Json(true);
        }



        #endregion Breadcrumb

        #region Helpers

        public ActionResult GetAllNames()
        {
            return Json(getAllNames(), JsonRequestBehavior.AllowGet);
        }

        #endregion Helpers

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

        #endregion Sessions
    }
}