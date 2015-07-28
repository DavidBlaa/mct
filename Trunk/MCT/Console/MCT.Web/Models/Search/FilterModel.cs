using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCT.DB.Entities;

namespace MCT.Web.Models.Search
{
    public class FilterModel
    {
        public List<string> Months { get; set; }

        public String SelectedSowing { get; set; }
        public String SelectedHarvest { get; set; }
        public String SelectedBloom { get; set; }
        public String SelectedSeedMaturity { get; set; }

        public FilterModel()
        {
            Months = TimePeriodHelper.GetMonthsAsStringList();
        }
    }
}