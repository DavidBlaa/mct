using MCT.Cal;
using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        #region calendar

        public JsonResult GetEvents()
        {
            SubjectManager subjectmanager = new SubjectManager();

            var events = new List<object>();

            foreach (var VARIABLE in subjectmanager.GetAll<TimePeriod>())
            {
                if (VARIABLE != null)
                    events.Add(getEventFromTimeperiod(VARIABLE));
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object getEventFromTimeperiod(TimePeriod tp)
        {
            string color = "";

            Debug.WriteLine(tp);

            if (tp is Sowing) color = "Green";
            if (tp is Harvest) color = "Red";
            if (tp is Bloom) color = "Blue";
            if (tp is SeedMaturity) color = "Yellow";
            if (tp is Cultivate) color = "Gray";
            if (tp is LifeTime) color = "YellowGreen";
            if (tp is Implant) color = "Purple";

            var json = new
            {
                title = tp.AssignedTo.Name,
                start = TimeConverter.GetStartDateTime((int)tp.StartMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")),
                end = TimeConverter.GetEndDateTime((int)tp.EndMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")),
                backgroundColor = color
            };

            Debug.WriteLine(json);

            if (json != null)
                return json;

            return null;
        }

        #endregion calendar

        #region Gantt

        public JsonResult GetEventsForGantt()
        {
            SubjectManager subjectmanager = new SubjectManager();

            var events = new List<object>();

            foreach (var plant in subjectmanager.GetAll<Plant>().ToList().OrderBy(p => p.Name))
            {
                if (plant != null)
                {
                    events.AddRange(GantHelper.GetAllEventsFromSubject(plant));
                }
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        #endregion Gantt
    }
}