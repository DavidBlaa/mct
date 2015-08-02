using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Lucene.Net.Util;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Search;
using MCT.Web.Models.Search;
using NHibernate;
using NHibernate.Linq;

namespace MCT.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [HttpGet]
        public ActionResult Search()
        {
            SubjectManager subjectManager = new SubjectManager();

            //Get all subjects
            var species = subjectManager.GetAll<Species>();

            SearchModel Model = new SearchModel();

            ResetSearchProvider();

            return View("Search",Model);
        }

        // GET: Search
        [HttpPost]
        public ActionResult Search(string searchValue)
        {
            SearchProvider sp = GetSearchProvider();

            if (string.IsNullOrEmpty(searchValue))
                sp.DeleteSearchCriterias(SearchProvider.FREETEXT_SEARCH_KEY);
            else
                sp.UpateSearchCriterias(SearchProvider.FREETEXT_SEARCH_KEY, searchValue);

            Debug.WriteLine("SEARCH : "+searchValue);

            List<SpeciesModel> Model = new List<SpeciesModel>();
            SubjectManager subjectManager = new SubjectManager();

            //Get filtered subjects
            //var species = sp.Search();

            SearchManager searchManager = new SearchManager();

            var species = searchManager.Search(sp.SearchCriterias);
            if (species != null)
            {
                //convert all subjects to subjectModels
                species = species.OrderBy(p => p.Name);
                species.ToList().ForEach(s => Model.Add(SpeciesModel.Convert(s)));
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
            List<SpeciesModel> Model = new List<SpeciesModel>();
            SearchProvider sp = GetSearchProvider();
            SearchManager searchManager = new SearchManager();

            var species = searchManager.Search(sp.SearchCriterias);

            if (species != null)
            {
                //convert all subjects to subjectModels
                species = species.OrderBy(p => p.Name);
                species.ToList().ForEach(s => Model.Add(SpeciesModel.Convert(s)));
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

        #region Show Data

            public ActionResult Details(long id, string type)
            {
                SubjectManager sm = new SubjectManager();

                Subject s = sm.Get(id);

                switch (type)
                {
                    case "Plant":
                        {

                            Plant plant = sm.GetAll<Plant>().Where(p => p.Id.Equals(id)).FirstOrDefault();

                            PlantModel Model = PlantModel.Convert(plant);
                            //load interactions
                            Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(plant).ToList());

                            return View("PlantDetails", Model);
                        }
                    case "Animal":
                        {
                            Animal animal = sm.GetAll<Animal>().Where(a => a.Id.Equals(id)).FirstOrDefault();

                            AnimalModel Model = AnimalModel.Convert(animal);
                            Model.Interactions = SubjectModel.ConverInteractionModels(sm.GetAllDependingInteractions(animal).ToList());

                            return View("AnimalDetails", Model);
                        }
                    case "Effect":
                        {
                            Effect effect = sm.GetAll<Effect>().Where(e => e.Id.Equals(id)).FirstOrDefault();

                            return View("EffectDetails");
                        }

                    default: { break; }
                }

                return View("Search");
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


        #endregion
    }
}