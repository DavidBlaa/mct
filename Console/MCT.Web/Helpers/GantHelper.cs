using MCT.Cal;
using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MCT.Web.Helpers
{
    public class GantHelper
    {
        public static List<object> GetAllEventsFromSubject(Subject subject)
        {
            List<object> events = new List<object>();

            if (subject != null)
            {
                if (subject.TimePeriods.Any(t => t is Bloom))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is Bloom),
                        TimePeriodType.Bloom));
                if (subject.TimePeriods.Any(t => t is Sowing))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is Sowing),
                        TimePeriodType.Sowing));
                if (subject.TimePeriods.Any(t => t is Harvest))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is Harvest),
                        TimePeriodType.Harvest));

                if (subject.TimePeriods.Any(t => t is SeedMaturity))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is SeedMaturity),
                        TimePeriodType.SeedMaturity));
                if (subject.TimePeriods.Any(t => t is Cultivate))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is Cultivate),
                        TimePeriodType.Cultivate));
                if (subject.TimePeriods.Any(t => t is Implant))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is Implant),
                        TimePeriodType.Implant));
                if (subject.TimePeriods.Any(t => t is LifeTime))
                    events.Add(GantHelper.CreateEventForEachPlantsTimeperiodType(
                        subject.Name,
                        subject.TimePeriods.Where(t => t is LifeTime),
                        TimePeriodType.Implant));
            }

            return events;
        }

        public static object CreateEventForEachPlantsTimeperiodType(string plantName, IEnumerable<TimePeriod> timePeriods, TimePeriodType type)
        {
            var tps = new List<object>();

            foreach (var VARIABLE in timePeriods)
            {
                if (VARIABLE != null)
                    tps.Add(GetEventFromTimeperiodForGantt(VARIABLE));
            }

            var json = new
            {
                name = plantName,
                desc = type.ToString(),
                values = tps
            };

            return json;
        }

        public static object GetEventFromTimeperiodForGantt(TimePeriod tp)
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
            var fromDT = TimeConverter.GetStartDateTime((int)tp.StartMonth, tp.StartArea).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
            var toDT = TimeConverter.GetEndDateTime((int)tp.EndMonth, tp.EndArea).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));

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
    }
}