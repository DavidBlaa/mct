namespace MCT.DB.Entities
{
    public class Sowing:TimePeriod
    {
        public Sowing()
        {

        }

        public Sowing(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
