namespace MCT.DB.Entities
{
    public class Cultivate : TimePeriod
    {

        public virtual int GerminationPeriodDays { get; set; }
        public virtual double GerminationTemperature { get; set; }



        public Cultivate()
        {

        }

        public Cultivate(string startDateText, string endDateText, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }


}
