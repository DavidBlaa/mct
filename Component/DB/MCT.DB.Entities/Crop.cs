namespace MCT.DB.Entities
{
    public class Crop : TimePeriod
    {
        public Crop()
        {

        }

        public Crop(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
