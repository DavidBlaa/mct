using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Node = MCT.DB.Entities.Node;

namespace MCT.Web.Controllers
{
    public class InteractionController : Controller
    {
        public static string ALL_SUBJECTS = "ALL_SUBJECTS";
        public static string ALL_SCIENTIFIC_NAMES = "ALL_SCIENTIFIC_NAMES";
        public static string ALL_PREDICATES = "ALL_PREDICATES";

        // GET: Interaction
        public ActionResult ShowInteractions()
        {

            return View("Interactions");
        }

        [HttpPost]
        public ActionResult Load(DataTableRecieverModel model)
        {
            try
            {
                string searchValue = Request["search[value]"];
                string orderByIndex = Request["order[0][column]"];
                string direction = Request["order[0][dir]"];

                //"columns[1][name]"
                string orderBy = Request["columns[" + orderByIndex + "][name]"];

                InteractionManager interactionManager = new InteractionManager();

                int skip = model.start;

                IQueryable<Interaction> data = null;

                data = interactionManager.GetAllAsQueryable<Interaction>();

                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    data = data.Where(i =>
                        (i.Subject != null && i.Subject.Name.ToLower().Contains(searchValue)) ||
                        (i.Object != null && i.Object.Name.ToLower().Contains(searchValue)) ||
                        (i.Predicate != null && i.Predicate.Name.ToLower().Contains(searchValue)) ||
                        (i.ImpactSubject != null && i.ImpactSubject.Name.ToLower().Contains(searchValue)));


                }


                //order by
                if (!string.IsNullOrEmpty(orderByIndex) && !string.IsNullOrEmpty(direction))
                {
                    data = data;
                }

                //paging
                data = data.Skip(skip).Take(model.length);


                int countAll = interactionManager.GetAllAsQueryable<Interaction>().Count();

                DataTableSendModel sendModel = new DataTableSendModel();
                sendModel.data = data.Select(InteractionSimpleModel.Convert).ToList();
                sendModel.draw = model.draw;
                sendModel.recordsTotal = countAll;
                sendModel.recordsFiltered = data.Count();

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




            return View("InteractionEdit", model);
        }

        public ActionResult Delete(long Id)
        {



            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id = 0)
        {
            if (id == 0) return View("InteractionEdit", new InteractionSimpleModel());

            InteractionManager interactionManager = new InteractionManager();
            Interaction interaction = interactionManager.Get(id);

            return View("InteractionEdit", InteractionSimpleModel.Convert(interaction));
        }

        [HttpGet]
        public ActionResult CheckSubject(string subject)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Node>().Any(n => n.Name.Equals(subject))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckObject(string Object)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Node>().Any(n => n.Name.Equals(Object))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckImpactSubject(string impactSubject)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Node>().Any(n => n.Name.Equals(impactSubject))) return Json(true, JsonRequestBehavior.AllowGet);

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

        public ActionResult GetAllScientificNamesResult()
        {
            return Json(getAllScientficNames(), JsonRequestBehavior.AllowGet);
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