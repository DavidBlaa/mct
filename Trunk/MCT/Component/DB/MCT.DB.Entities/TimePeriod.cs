using System;
using System.Linq;

namespace MCT.DB.Entities
{
    public abstract class TimePeriod
    {
        public virtual long Id { get; set; }
        public virtual long Subject { get; set; }
        public virtual TimePeriodArea StartArea { get; set; }
        public virtual TimePeriodMonth StartMonth { get; set; }
        public virtual TimePeriodArea EndArea { get; set; }
        public virtual TimePeriodMonth EndMonth { get; set; }
        public virtual TimePeriodType Type { get; set; }

        public virtual Subject AssignedTo { get; set; }
            


        protected void setParameters(string startDateText, string endDateText, TimePeriodType type)
        {
             Type = type;

             string[] start = startDateText.Split(' ');

             if (start.Count() == 0)
                 throw new Exception("Start date is not Valid");

             if (start.Count() == 1)
             {
                 this.StartMonth = TimePeriodHelper.GetMonth(start[0]);
             }

             if (start.Count() == 2)
             {
                 this.StartArea = TimePeriodHelper.GetTimeArea(start[0]);
                 this.StartMonth = TimePeriodHelper.GetMonth(start[1]);
             }

             string[] end = endDateText.Split(' ');

             if (end.Count() == 0)
                 throw new Exception("Start date is not Valid");

             if (end.Count() == 1)
             {
                 this.EndMonth = TimePeriodHelper.GetMonth(end[0]);
             }

             if (end.Count() == 2)
             {
                 this.EndArea = TimePeriodHelper.GetTimeArea(end[0]);
                 this.EndMonth = TimePeriodHelper.GetMonth(end[1]);
             }



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
                tpString += StartArea +" ";

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
            if (Enum.TryParse<TimePeriodArea>(value, out result))
            {
                return result;
            }

            return TimePeriodArea.Voll;
        }

        public static TimePeriodMonth GetMonth(string value)
        {
            TimePeriodMonth result;
            if (Enum.TryParse<TimePeriodMonth>(value, out result))
            {
                return result;
            }

            return TimePeriodMonth.Januar;
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
        Januar  = 1,
        Februar = 2,
        März    = 3,
        April   = 4,
        Mai     = 5,
        Juni    = 6,
        Juli    = 7,
        August  = 8,
        September = 9,
        Oktober = 10,
        November= 11,
        Dezember=12,
    }

    public enum TimePeriodType
    { 
        Bloom,
        Harvest,
        Sowing,
        SeedMaturity
    }
}
