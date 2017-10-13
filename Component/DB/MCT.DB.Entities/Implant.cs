namespace MCT.DB.Entities
{
    public class Implant : TimePeriod
    {
        public Implant()
        {

        }

        public Implant(string startDateText, string endDateText, TimePeriodType type, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }
}
