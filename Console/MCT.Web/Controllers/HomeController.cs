using MCT.Web.Helpers;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(ModelHelper.GetStatistics());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}