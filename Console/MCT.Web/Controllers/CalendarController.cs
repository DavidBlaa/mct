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

            foreach (var plant in subjectmanager.GetAll<Plant>().ToList().OrderBy(p => p.Name))
            {
                if (plant != null)
                {
                    if(plant.Bloom.Any())   
                        events.Add(createEventForEachPlantsTimeperiodType(plant,TimePeriodType.Bloom));
                    if (plant.Sowing.Any())
                        events.Add(createEventForEachPlantsTimeperiodType(plant,TimePeriodType.Sowing));
                    if (plant.Harvest.Any())
                        events.Add(createEventForEachPlantsTimeperiodType(plant,TimePeriodType.Harvest));
                    if (plant.SeedMaturity.Any())
                        events.Add(createEventForEachPlantsTimeperiodType(plant,TimePeriodType.SeedMaturity));
                }
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object createEventForEachPlantsTimeperiodType(Plant plant,TimePeriodType type)
        {
            var tps = new List<object>();

            switch (type)
            {
                case TimePeriodType.Bloom:
                    {
                        foreach (var VARIABLE in plant.Bloom)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                   
                case TimePeriodType.Harvest:
                    {
                        foreach (var VARIABLE in plant.Harvest)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                case TimePeriodType.Sowing:
                    {
                        foreach (var VARIABLE in plant.Sowing)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                case TimePeriodType.SeedMaturity:
                    {
                        foreach (var VARIABLE in plant.SeedMaturity)
                        {
                            if (VARIABLE != null)
                                tps.Add(getEventFromTimeperiodForGantt(VARIABLE, VARIABLE.Type));
                        }

                        break;
                    }
                default:
                    break;
            }

            
            var json = new
            {
                name = plant.Name,
                desc = type.ToString(),
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
                case TimePeriodType.Bloom: { color = "Orange"; break; }
                case TimePeriodType.SeedMaturity: { color = "Red"; break; }
            }
            //ToDo datetime is not focusing on the on voll, anfang, end
            var fromDT = TimeConverter.GetStartDateTime((int)tp.StartMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
            var toDT = TimeConverter.GetEndDateTime((int)tp.EndMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));

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