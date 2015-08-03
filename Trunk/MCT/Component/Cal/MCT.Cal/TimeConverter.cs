using System;

namespace MCT.Cal
{
    public class TimeConverter
    {
        public static DateTime GetStartDateTime(int monthIndex)
        {
            DateTime temp = new DateTime(DateTime.Today.Year,monthIndex+1,1);
            return temp;
        }

        public static DateTime GetEndDateTime(int monthIndex)
        {
            DateTime temp = new DateTime(DateTime.Today.Year, monthIndex + 1, DateTime.DaysInMonth(DateTime.Today.Year,monthIndex + 1));
            return temp;
        }
    }
}
