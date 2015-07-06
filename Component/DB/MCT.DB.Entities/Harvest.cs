namespace MCT.DB.Entities
{
    public class Harvest:TimePeriod
    {
        public Harvest()
        {

        }

        public Harvest(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
