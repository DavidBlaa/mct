using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCT.DB.Entities;

namespace MCT.Web.Models.Search
{
    public class FilterModel
    {
        public List<FilterDropdownElement> Months { get; set; }

        public int SelectedSowing { get; set; }
        public int SelectedHarvest { get; set; }
        public int SelectedBloom { get; set; }
        public int SelectedSeedMaturity { get; set; }

        public FilterModel()
        {
            Months = createMonthDropdownList();
        }


        private List<FilterDropdownElement> createMonthDropdownList()
        {
            List<FilterDropdownElement> temp = new List<FilterDropdownElement>();

            foreach (var Id in TimePeriodHelper.GetMonthsAsIdList())
            {
                temp.Add(new FilterDropdownElement()
                {
                    Id = Id,
                    Name = TimePeriodHelper.GetMonthName(Id)

                });
            }

            return temp;
        }
    }

    public class FilterDropdownElement
    {
        public int Id { get; set; }
        public string Name{get; set; }
    }
}