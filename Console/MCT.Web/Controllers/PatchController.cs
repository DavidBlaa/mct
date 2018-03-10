using MCT.DB.Entities.PatchPlaner;
using MCT.DB.Services;
using MCT.Web.Helpers.PatchPlaner;
using MCT.Web.Models.PatchPlaner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class PatchController : Controller
    {
        // GET: Patch
        public ActionResult Index()
        {
            PatchManager patchManager = new PatchManager();

            var patches = patchManager.GetAll<Patch>();

            List<PatchModel> model = new List<PatchModel>();

            foreach (var patch in patches)
            {
                model.Add(PatchModelHelper.ConvertTo(patch));
            }

            return View("Patches", model);
        }

        public ActionResult Create()
        {
            return View("PatchEdit", new PatchModel());
        }

        public ActionResult Edit(long id)
        {
            if (id <= 0) return View("PatchEdit", new PatchModel());


            PatchManager patchManager = new PatchManager();

            var patch = patchManager.Get(id);

            var model = PatchModelHelper.ConvertTo(patch);

            return View("PatchEdit", model);
        }

        public ActionResult Save(PatchModel model)
        {
            if (model.Id == 0)
            {
                //create
                Patch p = PatchModelHelper.ConvertTo(model);
                PatchManager patchManager = new PatchManager();
                patchManager.Create(p);

                return RedirectToAction("Index", "Patch");
            }
            else
            {
                //update

                Patch p = PatchModelHelper.ConvertTo(model);
                PatchManager patchManager = new PatchManager();
                Patch patch = patchManager.Get(p.Id);

                if (patch != null)
                {
                    patch.Height = p.Height;
                    patch.Width = p.Width;
                    patch.NutrientClaim = p.NutrientClaim;
                    patch.LocationType = p.LocationType;
                    patch.Name = p.Name;
                    patch.Description = p.Description;

                    patchManager.Update(patch);
                }
            }

            return View("PatchEdit", model);
        }

        public ActionResult Delete(long id)
        {
            if (id > 0)
            {
                PatchManager patchManager = new PatchManager();

                Patch patch = patchManager.Get(id);
                patchManager.Delete(patch);
            }

            return RedirectToAction("Index", "Patch");
        }

        public ActionResult Details(long id)
        {
            return RedirectToAction("Index", "PatchPlaner", new { id });
        }
    }
}