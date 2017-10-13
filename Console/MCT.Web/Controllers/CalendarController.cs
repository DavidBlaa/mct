using MCT.Cal;
using MCT.DB.Entities;
using MCT.DB.Services;
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
                    if (plant.TimePeriods.Any(t => t is Bloom))
                        events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Bloom));
                    if (plant.TimePeriods.Any(t => t is Sowing))
                        events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Sowing));
                    if (plant.TimePeriods.Any(t => t is Harvest))
                        events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Harvest));
                    if (plant.TimePeriods.Any(t => t is SeedMaturity))
                        events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.SeedMaturity));
                    if (plant.TimePeriods.Any(t => t is Cultivate))
                        events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Cultivate));
                    if (plant.TimePeriods.Any(t => t is Implant))
                        events.Add(createEventForEachPlantsTimeperiodType(plant, TimePeriodType.Implant));
                }
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object createEventForEachPlantsTimeperiodType(Plant plant, TimePeriodType type)
        {
            var tps = new List<object>();

            foreach (var VARIABLE in plant.TimePeriods)
            {
                if (VARIABLE != null)
                    tps.Add(getEventFromTimeperiodForGantt(VARIABLE));
            }

            var json = new
            {
                name = plant.Name,
                desc = type.ToString(),
                values = tps
            };

            return json;
        }

        private object getEventFromTimeperiodForGantt(TimePeriod tp)
        {
            string color = "Black";

            Debug.WriteLine(tp);


            if (tp is Sowing) color = "Green";
            if (tp is Harvest) color = "Red";
            if (tp is Bloom) color = "Blue";
            if (tp is SeedMaturity) color = "Yellow";
            if (tp is Cultivate) color = "Gray";
            if (tp is LifeTime) color = "YellowGreen";
            if (tp is Implant) color = "Purple";


            //ToDo datetime is not focusing on the on voll, anfang, end
            var fromDT = TimeConverter.GetStartDateTime((int)tp.StartMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
            var toDT = TimeConverter.GetEndDateTime((int)tp.EndMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));

            var tpJSON = new
            {
                label = tp.GetType().FullName,
                from = "/Date(" + fromDT + ")/",
                to = "/Date(" + toDT + ")/",
                customClass = "gantt" + color
            };

            Debug.WriteLine(tpJSON);

            if (tpJSON != null)
                return tpJSON;

            return null;
        }

        #endregion Gantt
    }
}