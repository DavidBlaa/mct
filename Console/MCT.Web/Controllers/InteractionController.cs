﻿using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class InteractionController : Controller
    {
        public static string ALL_SUBJECTS = "ALL_SUBJECTS";
        public static string ALL_PREDICATES = "ALL_PREDICATES";

        // GET: Interaction
        public ActionResult ShowInteractions()
        {
            Session[ALL_SUBJECTS] = null;
            Session[ALL_PREDICATES] = null;

            return View("Interactions");
        }

        [HttpPost]
        public ActionResult Load(DataTableRecieverModel model)
        {
            try
            {
                InteractionManager interactionManager = new InteractionManager();

                int skip = model.Start;

                IQueryable<Interaction> data = null;

                data = interactionManager.GetAllAsQueryable<Interaction>();

                //search
                if (!string.IsNullOrEmpty(model.Search.Value))
                {
                    string searchValue = model.Search.Value.ToLower();
                    data = data.Where(i =>
                        (i.Subject != null && i.Subject.Name.ToLower().Contains(searchValue)) ||
                        (i.Object != null && i.Object.Name.ToLower().Contains(searchValue)) ||
                        (i.Predicate != null && i.Predicate.Name.ToLower().Contains(searchValue)) ||
                        (i.ImpactSubject != null && i.ImpactSubject.Name.ToLower().Contains(searchValue)));
                }

                int filteredRows = data.ToList().Count();

                //order by
                var sorting = string.Join(",", model.Order.Select(o => model.Columns[o.Column].Data + " " + o.Dir));
                if (!string.IsNullOrEmpty(sorting))
                {
                    data = data.OrderBy(sorting);
                }

                //paging
                data = data.Skip(skip).Take(model.Length);

                int countAll = interactionManager.GetAllAsQueryable<Interaction>().Count();

                DataTableSendModel sendModel = new DataTableSendModel();
                sendModel.data = data.Select(InteractionModel.Convert).ToList();
                sendModel.draw = model.Draw;
                sendModel.recordsTotal = countAll;
                sendModel.recordsFiltered = filteredRows;

                return Json(sendModel);
            }
            catch (Exception exception)
            {
                string json = "{\"error\":\"" + exception.Message + "\"}";

                return Json(json);
            }
        }

        public ActionResult Save(InteractionSimpleModel model)
        {
            SubjectManager subjectManager = new SubjectManager();
            InteractionManager interactionManager = new InteractionManager();

            IEnumerable<Subject> all = subjectManager.GetAll<Subject>();
            IEnumerable<Predicate> allPredicates = subjectManager.GetAll<Predicate>();

            Interaction targetInteraction = new Interaction();

            //ToDO Check if all interactions null

            if (model.Id > 0)
            {
                targetInteraction = interactionManager.Get(model.Id);
            }

            //check if all entities has a 0 id, then it needs to create first
            if (!String.IsNullOrEmpty(model.Subject))
            {
                if (all.Where(s => s.Name.Equals(model.Subject)).Any())
                {
                    var obj = all.Where(s => s.Name.Equals(model.Subject)).FirstOrDefault();
                    targetInteraction.Subject = obj;
                }
            }

            if (!String.IsNullOrEmpty(model.Object))
            {
                if (all.Where(s => s.Name.Equals(model.Object)).Any())
                {
                    var obj = all.Where(s => s.Name.Equals(model.Object)).FirstOrDefault();
                    targetInteraction.Object = obj;
                }
            }

            if (!String.IsNullOrEmpty(model.Predicate))
            {
                if (allPredicates.Where(s => s.Name.Equals(model.Predicate)).Any())
                {
                    var obj = allPredicates.Where(s => s.Name.Equals(model.Predicate)).FirstOrDefault();
                    targetInteraction.Predicate = obj;
                }
            }

            if (!String.IsNullOrEmpty(model.ImpactSubject))
            {
                if (all.Where(s => s.Name.Equals(model.ImpactSubject)).Any())
                {
                    var obj = all.Where(s => s.Name.Equals(model.ImpactSubject)).FirstOrDefault();
                    targetInteraction.ImpactSubject = obj;
                }
            }

            if (model.Indicator > -1)
            {
                targetInteraction.Indicator = model.Indicator;
                ViewData["Status"] = "gespeichert";
            }

            try
            {
                interactionManager.Update(targetInteraction);
                model = InteractionSimpleModel.Convert(targetInteraction);
            }
            catch (Exception ex)
            {
                ViewData["Status"] = ex.Message;
            }

            return View("InteractionEdit", model);
        }

        public ActionResult Delete(long id)
        {
            if (id > 0)
            {
                InteractionManager interactionManager = new InteractionManager();
                Interaction interaction = interactionManager.Get(id);
                interactionManager.Delete(interaction);
            }
            return RedirectToAction("ShowInteractions", "Interaction");
        }

        public ActionResult Edit(long id = 0)
        {
            if (id == 0) return View("InteractionEdit", new InteractionSimpleModel());

            InteractionManager interactionManager = new InteractionManager();
            Interaction interaction = interactionManager.Get(id);

            getAllPredicateNames();

            return View("InteractionEdit", InteractionSimpleModel.Convert(interaction));
        }

        [HttpGet]
        public ActionResult CheckSubject(string subject)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Subject>().Any(n => n.Name.Equals(subject))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckObject(string Object)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Subject>().Any(n => n.Name.Equals(Object))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckImpactSubject(string impactSubject)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Subject>().Any(n => n.Name.Equals(impactSubject))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckPredicate(string predicate)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Predicate>().Any(n => n.Name.Equals(predicate))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmptyInteraction()
        {
            return PartialView("InteractionEdit", new InteractionModel());
        }

        public ActionResult GetAllSubjects()
        {
            return Json(getAllSubjectNames(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllPredicates()
        {
            return Json(getAllPredicateNames(), JsonRequestBehavior.AllowGet);
        }

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

        private List<string> getAllPredicateNames()
        {
            if (Session[ALL_PREDICATES] == null)
            {
                SubjectManager subjectManager = new SubjectManager();

                //ToDo change the position of load this
                //load Viewdata
                // load all subject for autocomplete
                Session[ALL_PREDICATES] = subjectManager.GetAll<Predicate>().Select(s => s.Name).ToList();
                //Session[ALL_PREDICATES] = subjectManager.GetAll<Predicate>().Select(s => s.Name);
            }

            List<string> tmp = Session[ALL_PREDICATES] as List<string>;

            return tmp;
        }

        #endregion Session
    }
}