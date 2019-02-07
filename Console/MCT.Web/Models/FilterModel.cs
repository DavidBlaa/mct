using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MCT.Web.Models
{
    public class FilterModel
    {
        public List<FilterDropdownElement> Months { get; set; }

        [EnumDataType(typeof(NutrientClaim))]
        public NutrientClaim? NutrientClaim { get; set; }

        [EnumDataType(typeof(RootDepth))]
        public RootDepth? RootDepths { get; set; }

        public List<FilterDropdownElement> SubjectList { get; set; }

        public int SelectedSowing { get; set; }
        public int SelectedHarvest { get; set; }
        public int SelectedBloom { get; set; }
        public int SelectedSeedMaturity { get; set; }

        public int SelectedNutrientClaim { get; set; }
        public int SelectedRootDepth { get; set; }

        public int SelectedPositiveInteraction { get; set; }
        public int SelectedNegativeInteraction { get; set; }

        public FilterModel(List<Subject> subjects)
        {
            Months = getMonthDropdownList();
            SubjectList = getSubjectsList(subjects);
        }

        private List<FilterDropdownElement> getSubjectsList(List<Subject> subjects)
        {
            List<FilterDropdownElement> temp = new List<FilterDropdownElement>();

            foreach (Subject s in subjects)
            {
                temp.Add(new FilterDropdownElement()
                {
                    Id = s.Id,
                    Name = s.Name
                });
            }

            return temp;
        }

        private List<FilterDropdownElement> getMonthDropdownList()
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
        public long Id { get; set; }
        public string Name { get; set; }
    }
}