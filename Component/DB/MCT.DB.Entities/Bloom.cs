namespace MCT.DB.Entities
{
    public class Bloom : TimePeriod
    {
        public Bloom()
        {

        }

        public Bloom(string startDateText, string endDateText, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }
}
