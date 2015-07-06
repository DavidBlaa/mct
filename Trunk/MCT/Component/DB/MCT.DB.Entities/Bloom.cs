namespace MCT.DB.Entities
{
    public class Bloom:TimePeriod
    {
        public Bloom()
        {

        }

        public Bloom(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
