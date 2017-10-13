namespace MCT.DB.Entities
{
    public class SeedMaturity : TimePeriod
    {
        public SeedMaturity()
        {

        }

        public SeedMaturity(string startDateText, string endDateText, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }
}
