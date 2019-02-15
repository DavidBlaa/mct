using MCT.Cal;
using MCT.DB.Entities;
using MCT.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                desc = type.GetAttribute<DisplayAttribute>().Name,
                values = tps
            };

            return json;
        }

        public static object GetEventFromTimeperiodForGantt(TimePeriod tp)
        {
            string color = "Black";
            string name = "no name";
            Debug.WriteLine(tp);

            if (tp is Sowing)
            {
                color = "Green";
                name = TimePeriodType.Sowing.GetAttribute<DisplayAttribute>().Name;
            }
            if (tp is Harvest)
            {
                color = "Red";
                name = TimePeriodType.Harvest.GetAttribute<DisplayAttribute>().Name;
            }
            if (tp is Bloom)
            {
                color = "Blue";
                name = TimePeriodType.Bloom.GetAttribute<DisplayAttribute>().Name;
            }
            if (tp is SeedMaturity)
            {
                color = "Yellow";
                name = TimePeriodType.SeedMaturity.GetAttribute<DisplayAttribute>().Name;
            }

            if (tp is Cultivate)
            {
                color = "Gray";
                name = TimePeriodType.Cultivate.GetAttribute<DisplayAttribute>().Name;
            }

            if (tp is LifeTime)
            {
                color = "YellowGreen";
                name = TimePeriodType.LifeTime.GetAttribute<DisplayAttribute>().Name;
            }
            if (tp is Implant)
            {
                color = "Purple";
                name = TimePeriodType.Implant.GetAttribute<DisplayAttribute>().Name;
            }

            var fromDT = TimeConverter.GetStartDateTime((int)tp.StartMonth, tp.StartArea).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
            var toDT = TimeConverter.GetEndDateTime((int)tp.EndMonth, tp.EndArea).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));

            var tpJSON = new
            {
                label = name,
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