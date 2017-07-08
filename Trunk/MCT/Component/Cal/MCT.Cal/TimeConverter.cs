using System;

namespace MCT.Cal
{
    public class TimeConverter
    {
        public static DateTime GetStartDateTime(int monthIndex)
        {

            DateTime temp = new DateTime(DateTime.Today.Year,monthIndex,1);
            return temp;
        }

        public static DateTime GetEndDateTime(int monthIndex)
        {
            if (monthIndex < 12)
                monthIndex += 1;

            DateTime temp = new DateTime(DateTime.Today.Year, monthIndex, DateTime.DaysInMonth(DateTime.Today.Year,monthIndex));
            return temp;
        }
    }
}
