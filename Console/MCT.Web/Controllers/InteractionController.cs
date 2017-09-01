using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Node = MCT.DB.Entities.Node;

namespace MCT.Web.Controllers
{
    public class InteractionController : Controller
    {
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
                InteractionManager interactionManager = new InteractionManager();

                int skip = model.start;

                var data = interactionManager.GetAllAsQueryable<Interaction>().Skip(skip).Take(model.length);
                int countAll = interactionManager.GetAllAsQueryable<Interaction>().Count();

                DataTableSendModel sendModel = new DataTableSendModel();
                sendModel.data = data.Select(InteractionViewModel.Convert).ToList();
                sendModel.draw = model.draw;
                sendModel.recordsTotal = countAll;
                sendModel.recordsFiltered = countAll;

                return Json(sendModel);
            }
            catch (Exception exception)
            {

                string json = "{\"error\":\"" + exception.Message + "\"}";

                return Json(json);
            }

        }

        public ActionResult Save(Interaction interaction)
        {



            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long Id)
        {



            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckNameOfSimpleLink(string name)
        {
            SubjectManager subjectManager = new SubjectManager();

            if (subjectManager.GetAll<Node>().Any(n => n.Name.Equals(name))) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmptyInteraction()
        {
            return PartialView("InteractionEdit", new InteractionModel());
        }
    }
}