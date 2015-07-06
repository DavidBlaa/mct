using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Search;
using MCT.Web.Models.Search;

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
            var subjects = subjectManager.GetAll<Subject>();

            SearchModel Model = new SearchModel();

            //convert all subjects to subjectModels
            subjects.ToList().ForEach(s => Model.Subjects.Add(SubjectModel.Convert(s)));

            return View("Search",Model);
        }

        // GET: Search
        [HttpPost]
        public ActionResult Search(string searchValue)
        {
            Debug.WriteLine("SEARCH : "+searchValue);

            List<SubjectModel> Model = new List<SubjectModel>();
            SubjectManager subjectManager = new SubjectManager();

            //Get filtered subjects
            var subjects = string.IsNullOrEmpty(searchValue) ? subjectManager.GetAll<Subject>() : SearchProvider.Search(searchValue);

            //convert all subjects to subjectModels
            subjects.ToList().ForEach(s => Model.Add(SubjectModel.Convert(s)));

            return PartialView("_searchResult", Model);
        
        }

        public ActionResult Details(long id, string type)
        {
            SubjectManager sm = new SubjectManager();

            Subject s = sm.Get(id);

            switch (type)
            {
                case "Plant": {

                    Plant plant = sm.GetAll<Plant>().Where(p => p.Id.Equals(id)).FirstOrDefault();
                    return View("PlantDetails", PlantModel.Convert(plant));
                }
                case "Animal": {
                    Animal animal = sm.GetAll<Animal>().Where(a => a.Id.Equals(id)).FirstOrDefault();
                    return View("AnimalDetails", AnimalModel.Convert(animal));
                }
                case "Effect": {
                    Effect effect = sm.GetAll<Effect>().Where(e => e.Id.Equals(id)).FirstOrDefault();
                    return View("EffectDetails");
                }

                default: { break; }
            }

            return View("Search");
        }
    }
}