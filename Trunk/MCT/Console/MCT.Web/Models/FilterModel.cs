using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCT.Web.Models
{
    public class FilterModel
    {
        public List<FilterDropdownElement> Months { get; set; }
        public List<FilterDropdownElement> NutrientClaims { get; set; }
        public List<FilterDropdownElement> RootDepths { get; set; }
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
            NutrientClaims = getNutrientClaimsDropdownList();
            RootDepths = getRootDepthsDropdownList();
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

        private List<FilterDropdownElement> getNutrientClaimsDropdownList()
        {
            List<FilterDropdownElement> temp = new List<FilterDropdownElement>();

            var allValues = (NutrientClaim[])Enum.GetValues(typeof(NutrientClaim));

            for (int i = 0; i < allValues.Length; i++)
            {
                var value = allValues.ElementAt(i);
                temp.Add(new FilterDropdownElement()
                {
                    Id = i,
                    Name = value.ToString()
                });

            }

            return temp;
        }

        private List<FilterDropdownElement> getRootDepthsDropdownList()
        {
            List<FilterDropdownElement> temp = new List<FilterDropdownElement>();

            var allValues = (RootDepth[])Enum.GetValues(typeof(RootDepth));

            for (int i = 0; i < allValues.Length; i++)
            {
                var value = allValues.ElementAt(i);
                temp.Add(new FilterDropdownElement()
                {
                    Id = i,
                    Name = value.ToString()
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