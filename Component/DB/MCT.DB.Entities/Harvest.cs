using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Harvest:TimePeriod
    {
        public Harvest()
        {

        }

        public Harvest(string startDateText, string endDateText, TimePeriodType type)
        {
            setParameters(startDateText, endDateText, type);
        }
    }
}
