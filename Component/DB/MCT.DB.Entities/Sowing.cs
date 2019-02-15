using NHibernate.Search.Attributes;

namespace MCT.DB.Entities
{
    [Indexed]
    public class Sowing : TimePeriod
    {
        public Sowing()
        {
        }

        public Sowing(string startDateText, string endDateText, TimePeriod next)
        {
            setParameters(startDateText, endDateText, next);
        }
    }
}