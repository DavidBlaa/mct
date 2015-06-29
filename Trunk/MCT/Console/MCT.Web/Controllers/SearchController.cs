using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search()
        {
            SubjectManager subjectManager = new SubjectManager();

            //Get all subjects
            var subjects = subjectManager.GetAll<Node>();

            SearchModel Model = new SearchModel();

            //convert all subjects to subjectModels
            subjects.ToList().ForEach(s => Model.Subjects.Add(SubjectModel.Convert(s)));

            return View(Model);
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