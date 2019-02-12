using MCT.DB.Entities;
using System;

namespace MCT.Cal
{
    public class TimeConverter
    {
        public static DateTime GetStartDateTime(int monthIndex, TimePeriodArea timePeriodArea)
        {
            int days = GetDayOfStartingTimePeriodArea(monthIndex, timePeriodArea);

            DateTime temp = new DateTime(DateTime.Today.Year, monthIndex, days);
            return temp;
        }

        public static DateTime GetEndDateTime(int monthIndex, TimePeriodArea timePeriodArea)
        {
            //if (monthIndex < 12)
            //    monthIndex += 1;
            int days = GetDayOfEndingTimePeriodArea(monthIndex, timePeriodArea);

            DateTime temp = new DateTime(DateTime.Today.Year, monthIndex, days);
            return temp;
        }

        public static int GetDayOfStartingTimePeriodArea(int month, TimePeriodArea area)
        {
            if (area.Equals(TimePeriodArea.Voll) || area.Equals(TimePeriodArea.Anfang))
            {
                return 1;
            }

            int daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, month);

            if (area.Equals(TimePeriodArea.Mitte))
                return (daysInMonth / 2) / 2;

            if (area.Equals(TimePeriodArea.Ende))
            {
                int half = daysInMonth / 2;
                int quarter = half / 2;
                int d = half + quarter;
                return d;
            }

            return 1;
        }

        public static int GetDayOfEndingTimePeriodArea(int month, TimePeriodArea area)
        {
            int daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, month);

            if (area.Equals(TimePeriodArea.Voll) || area.Equals(TimePeriodArea.Anfang))
            {
                int half = daysInMonth / 2;
                int quarter = half / 2;
                int d = half - quarter;
                return d;
            }

            if (area.Equals(TimePeriodArea.Mitte))
            {
                int half = daysInMonth / 2;
                int quarter = half / 2;
                int d = half + quarter;
                return d;
            }

            if (area.Equals(TimePeriodArea.Ende))
            {
                return daysInMonth;
            }

            return 1;
        }
    }
}