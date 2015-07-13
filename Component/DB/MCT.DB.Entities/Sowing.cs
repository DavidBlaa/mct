using NHibernate.Search.Attributes;

namespace MCT.DB.Entities
{
    [Indexed]
    public class Sowing:TimePeriod
    {
        
        public Sowing()
        {

        }

        public Sowing(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
