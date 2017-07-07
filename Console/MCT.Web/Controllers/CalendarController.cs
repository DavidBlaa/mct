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
                    events.Add(getEventFromTimeperiod(VARIABLE, VARIABLE.Type));
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object getEventFromTimeperiod(TimePeriod tp, TimePeriodType type)
        {
            string color = "";

            Debug.WriteLine(tp);

            switch (type)
            {
                case TimePeriodType.Sowing: { color = "green"; break; }
                case TimePeriodType.Harvest: { color = "blue"; break; }
                case TimePeriodType.Bloom: { color = "yellow"; break; }
                case TimePeriodType.SeedMaturity: { color = "red"; break; }
            }

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

            foreach (var VARIABLE in subjectmanager.GetAll<Plant>().ToList().OrderBy(p => p.Name))
            {
                if (VARIABLE != null)
                    events.Add(createPlants(VARIABLE));
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object createPlants(Plant plant)
        {
            var tps = new List<object>();

            foreach (var VARIABLE in plant.Bloom)
            {
                if (VARIABLE != null)
                    tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
            }

            foreach (var VARIABLE in plant.Sowing)
            {
                if (VARIABLE != null)
                    tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
            }

            foreach (var VARIABLE in plant.Harvest)
            {
                if (VARIABLE != null)
                    tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
            }

            foreach (var VARIABLE in plant.SeedMaturity)
            {
                if (VARIABLE != null)
                    tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
            }

            var json = new
            {
                name = plant.Name,
                desc = plant.ScientificName,
                values = tps
            };

            return json;
        }

        private object getEventFromTimeperiodForGantt(TimePeriod tp, TimePeriodType type)
        {
            string color = "";

            Debug.WriteLine(tp);

            switch (type)
            {
                case TimePeriodType.Sowing: { color = "Green"; break; }
                case TimePeriodType.Harvest: { color = "Blue"; break; }
                case TimePeriodType.Bloom: { color = "Yellow"; break; }
                case TimePeriodType.SeedMaturity: { color = "Red"; break; }
            }

            var fromDT = TimeConverter.GetStartDateTime((int)tp.StartMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
            var toDT = TimeConverter.GetStartDateTime((int)tp.EndMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));

            //name: "Testing",
            //    desc: " ",
            //    values: [{
            //    from: today_friendly,
            //        to: next_friendly,
            //        label: "Test",
            //        customClass: "ganttRed"
            //    }]


            var tpJSON = new
            {
                label = type.ToString(),
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