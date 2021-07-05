using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate.Search.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace MCT.DB.Entities
{
    [Indexed]
    public abstract class TimePeriod
    {
        [DocumentId]
        public virtual long Id { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TimePeriodArea StartArea { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TimePeriodMonth StartMonth { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TimePeriodArea EndArea { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TimePeriodMonth EndMonth { get; set; }

        public virtual Subject AssignedTo { get; set; }

        public virtual bool Start { get; set; }

        public virtual TimePeriod Next { get; set; }

        protected void setParameters(string startDateText, string endDateText, TimePeriod next)
        {
            string[] start = startDateText.Split(' ');

            if (start.Count() == 0)
                throw new Exception("Start date is not Valid");

            if (start.Count() == 1)
            {
                StartMonth = TimePeriodHelper.GetMonth(start[0]);
            }

            if (start.Count() == 2)
            {
                StartArea = TimePeriodHelper.GetTimeArea(start[0]);
                StartMonth = TimePeriodHelper.GetMonth(start[1]);
            }

            string[] end = endDateText.Split(' ');

            if (end.Count() == 0)
                throw new Exception("Start date is not Valid");

            if (end.Count() == 1)
            {
                EndMonth = TimePeriodHelper.GetMonth(end[0]);
            }

            if (end.Count() == 2)
            {
                EndArea = TimePeriodHelper.GetTimeArea(end[0]);
                EndMonth = TimePeriodHelper.GetMonth(end[1]);
            }

            if (next != null)
            {
                Next = next;
            }

            Start = false;
        }

        public static bool IsEmpty(TimePeriod tp)
        {
            if (tp.StartArea != null &&
               tp.StartMonth != null &&
               tp.EndArea != null &&
               tp.EndMonth != null)
                return false;

            return true;
        }

        public virtual string GetTimePeriodAsString()
        {
            string tpString = "";

            if (StartArea != TimePeriodArea.Voll)
                tpString += StartArea + " ";

            tpString += StartMonth;

            if (EndArea != TimePeriodArea.Voll)
                tpString += "-" + EndArea + " ";

            if (EndArea == TimePeriodArea.Voll)
                tpString += "-" + EndMonth;
            else
                tpString += EndMonth;

            return tpString;
        }
    }

    public class TimePeriodHelper
    {
        public static TimePeriodArea GetTimeArea(string value)
        {
            TimePeriodArea result;
            if (Enum.TryParse(value, out result))
            {
                return result;
            }

            return TimePeriodArea.Voll;
        }

        public static TimePeriodMonth GetMonth(string value)
        {
            TimePeriodMonth result;
            if (Enum.TryParse(value, out result))
            {
                return result;
            }

            return TimePeriodMonth.Januar;
        }

        public static TimePeriodMonth GetMonth(int id)
        {
            return (TimePeriodMonth)id;
        }

        public static string GetMonthName(int id)
        {
            return ((TimePeriodMonth)id).ToString();
        }

        public static int GetMonthIndex(string value)
        {
            TimePeriodMonth result;
            if (Enum.TryParse(value, out result))
            {
                return (int)result;
            }

            return (int)TimePeriodMonth.Januar;
        }

        public static List<string> GetMonthsAsStringList()
        {
            return Enum.GetValues(typeof(TimePeriodMonth))
                    .Cast<TimePeriodMonth>()
                    .Select(v => v.ToString())
                    .ToList();
        }

        public static List<int> GetMonthsAsIdList()
        {
            return Enum.GetValues(typeof(TimePeriodMonth))
                    .Cast<TimePeriodMonth>()
                    .Select(v => (int)v)
                    .ToList();
        }
    }

    public enum TimePeriodArea
    {
        Voll,
        Anfang,
        Mitte,
        Ende
    }

    public enum TimePeriodMonth
    {
        Januar = 1,
        Februar = 2,
        März = 3,
        April = 4,
        Mai = 5,
        Juni = 6,
        Juli = 7,
        August = 8,
        September = 9,
        Oktober = 10,
        November = 11,
        Dezember = 12
    }

    public enum TimePeriodType
    {
        [Display(Name = "Blühen")]
        [EnumMember(Value = "Blühen")]
        Bloom,

        [Display(Name = "Ernte")]
        [EnumMember(Value = "Ernten")]
        Harvest,

        [Display(Name = "Sähen")]
        [EnumMember(Value = "Sähen")]
        Sowing,

        [Display(Name = "Samen")]
        [EnumMember(Value = "Samen")]
        SeedMaturity,

        [Display(Name = "Vorziehen")]
        [EnumMember(Value = "Vorziehen")]
        Cultivate,

        [Display(Name = "Einpflanzen")]
        [EnumMember(Value = "Einpflanzen")]
        Implant,

        [Display(Name = "Lebensspanne")]
        [EnumMember(Value = "Lebensspanne")]
        LifeTime
    }
}