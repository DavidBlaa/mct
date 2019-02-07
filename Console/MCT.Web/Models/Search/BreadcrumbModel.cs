using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.Search
{
    public class BreadcrumbModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public string DisplayValue { get; set; }

        public BreadcrumbModel()
        {
            Key = string.Empty;
            Value = string.Empty;
            DisplayName = string.Empty;
            DisplayValue = string.Empty;
        }
    }
}