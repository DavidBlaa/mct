namespace MCT.DB.Entities
{
    public class SeedMaturity:TimePeriod
    {
        public SeedMaturity()
        {

        }

        public SeedMaturity(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
