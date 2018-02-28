using MCT.DB.Entities;
using MCT.DB.Entities.PatchPlaner;
using MCT.DB.Services;
using MCT.Web.Helpers.PatchPlaner;
using MCT.Web.Models.PatchPlaner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class PatchPlanerController : Controller
    {
        // GET: Patch
        public ActionResult Index()
        {
            return View();
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

    }
}