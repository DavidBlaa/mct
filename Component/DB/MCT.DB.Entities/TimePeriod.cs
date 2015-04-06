using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class TimePeriod
    {
        public virtual long Id { get; set; }
        public virtual TimePeriodArea StartArea { get; set; }
        public virtual TimePeriodMonth StartMonth { get; set; }
        public virtual TimePeriodArea EndArea { get; set; }
        public virtual TimePeriodMonth EndMonth { get; set; }

        public TimePeriod()
        {

        }

        public TimePeriod(string startDateText, string endDateText)
        {
            setParameters(startDateText, endDateText);
        }

        private void setParameters(string startDateText, string endDateText)
        {
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

            return TimePeriodArea.Full;
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
        Ganzer,
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
}
