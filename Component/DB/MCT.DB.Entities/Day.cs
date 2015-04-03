using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Day
    {
        public virtual long Id { get; set; }
        public virtual int DayInYear { get; set; }
        public virtual int WeekPerYear { get; set; }
    }
}
