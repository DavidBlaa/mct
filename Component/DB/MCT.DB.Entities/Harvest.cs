namespace MCT.DB.Entities
{
    public class Harvest : TimePeriod
    {
        public Harvest()
        {

        }

        public Harvest(string startDateText, string endDateText, TimePeriod next)
        {
            setParameters(startDateText, endDateText,next);
        }
    }
}
