using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCT.Cal;
using MCT.DB.Entities;
using MCT.DB.Services;

namespace MCT.Web.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            SubjectManager subjectmanager = new SubjectManager();

            var events=new List<object>();

            foreach (var VARIABLE in subjectmanager.GetAll<TimePeriod>())
            {
                events.Add(getEventFromTimeperiod(VARIABLE, VARIABLE.Type));
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private object getEventFromTimeperiod(TimePeriod tp, TimePeriodType type)
        {
            string color = "";

            switch (type)
            {
                case TimePeriodType.Sowing: {color = "green";break;}
                case TimePeriodType.Harvest:{color = "blue";break;}
                case TimePeriodType.Bloom:{color = "yellow";break;}
                case TimePeriodType.SeedMaturity:{color = "red";break;}
            }

            return
                new
                {
                    title = tp.AssignedTo.Name,
                    start = TimeConverter.GetStartDateTime((int) tp.StartMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")),
                    end = TimeConverter.GetEndDateTime((int) tp.EndMonth).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")),
                    backgroundColor = color
                };
        }
    }
}