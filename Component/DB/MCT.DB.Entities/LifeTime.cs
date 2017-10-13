namespace MCT.DB.Entities
{
    public class LifeTime : TimePeriod
    {
        public LifeTime()
        {

        }

        public LifeTime(string startDateText, string endDateText, TimePeriodType type, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }
}
