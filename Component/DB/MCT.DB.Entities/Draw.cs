namespace MCT.DB.Entities
{
    public class Draw : TimePeriod
    {
        public Draw()
        {

        }

        public Draw(string startDateText, string endDateText, TimePeriodType type, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }
}
