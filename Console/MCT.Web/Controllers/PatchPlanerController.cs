using MCT.DB.Entities.PatchPlaner;
using MCT.DB.Services;
using MCT.Web.Helpers.PatchPlaner;
using MCT.Web.Models.PatchPlaner;
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


    }
}