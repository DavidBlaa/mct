using MCT.DB.Entities;
using MCT.DB.Services;

namespace MCT.Web.Helpers
{
    public class SeedDataGenerator
    {
        public static void GenerateDays()
        {
            DateManager dateManager = new DateManager();
            Day day; 

            for (int i = 1; i <= 52; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    day = new Day();
                    day.DayInYear = i * j;
                    day.WeekPerYear = i;

                    dateManager.Create(day);
                }
            }

        }

    }
}