using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
