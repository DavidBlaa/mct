using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class PredicateController : Controller
    {
        // GET: Predicate
        public ActionResult Index()
        {
            SubjectManager subjectManager = new SubjectManager();
            List<PredicateModel> Model = new List<PredicateModel>();

            var predicates = subjectManager.GetAll<Predicate>();
            predicates.ToList().ForEach(p => Model.Add(PredicateModel.Convert(p)));

            return View(Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadSelectionOfParentPredicates();

            return View(new PredicateCreateModel());
        }

        [HttpPost]
        public ActionResult Create(PredicateCreateModel model)
        {
            if (model == null) return View(model);

            if (ModelState.IsValid)
            {
                PredicateManager predicateManager = new PredicateManager();

                Predicate newPredicate = new Predicate();
                newPredicate.Name = model.Name;
                newPredicate.Description = model.Name;

                //get parent
                var parent = predicateManager.Get(model.ParentId);

                newPredicate.Parent = parent;

                predicateManager.Create(newPredicate);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            LoadSelectionOfParentPredicates();

            PredicateManager predicateManager = new PredicateManager();
            var predicate = predicateManager.Get(id);
            if (predicate != null)
            {
                PredicateCreateModel model = new PredicateCreateModel();
                model.Id = predicate.Id;
                model.Name = predicate.Name;
                model.Description = predicate.Description;
                model.ParentId = predicate.Parent != null ? predicate.Parent.Id : 0;

                return View(model);
            }

            return Redirect("Create");
        }

        [HttpPost]
        public ActionResult Edit(PredicateCreateModel model)
        {
            if (model == null) { return View(model); }

            PredicateManager predicateManager = new PredicateManager();
            var predicate = predicateManager.Get(model.Id);

            if (predicate != null)
            {
                try
                {
                    predicate.Name = model.Name;
                    predicate.Description = model.Description;
                    //get parent
                    var parent = predicateManager.Get(model.ParentId);
                    predicate.Parent = parent;

                    predicateManager.Update(predicate);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ein Fehler ist aufgetreten. " + ex.Message);
                }
            }

            LoadSelectionOfParentPredicates();

            ModelState.AddModelError("", "Das Prädikat konnte nicht aktualisiert werden.");
            return View(model);
        }

        public ActionResult Delete(long id)
        {
            if (id > 0)
            {
                InteractionManager interactionManager = new InteractionManager();
                SubjectManager subjectManager = new SubjectManager();
                PredicateManager predicateManager = new PredicateManager();

                //get all interaction with this predicate
                var interactions = interactionManager.GetAll().Where(i => i.Predicate.Id.Equals(id));

                // delete the interactions
                foreach (var interaction in interactions)
                {
                    interactionManager.Delete(interaction);
                }

                // delete the predictae

                var predicate = predicateManager.Get(id);
                if (predicate != null)
                    predicateManager.Delete(predicate);
            }

            return RedirectToAction("Index");
        }

        private void LoadSelectionOfParentPredicates()
        {
            InteractionManager interactionManager = new InteractionManager();
            var rootPredicates = interactionManager.GetAll<Predicate>().Where(p => p.Parent == null);
            List<PredicateModel> parentSelectionList = new List<PredicateModel>();

            rootPredicates.ToList().ForEach(p => parentSelectionList.Add(PredicateModel.Convert(p)));
            ViewData["Parents"] = parentSelectionList;
        }

        [HttpGet]
        public ActionResult CheckPredicate(string name, string initName)
        {
            PredicateManager predicateManager = new PredicateManager();

            string defaultstring = "";
            if (!String.IsNullOrEmpty(initName)) defaultstring = initName;

            if (predicateManager.GetAll().Any(n => n.Name != null && n.Name.Equals(name) && !n.Name.Equals(initName))) return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}