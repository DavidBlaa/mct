using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models.Search;
using System;
using System.Collections.Generic;

namespace MCT.Web.Helpers
{
    public class ModelHelper
    {
        public static BreadcrumbModel ConvertTo(KeyValuePair<string, string> keyValuePair)
        {
            BreadcrumbModel model = new BreadcrumbModel();
            SubjectManager subjectManager = new SubjectManager();

            switch (keyValuePair.Key)
            {
                case "FREETEXT_SEARCH_KEY":
                    {
                        model.DisplayName = "Sucheingabe";
                        model.DisplayValue = keyValuePair.Value;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }

                case "NutrientClaim":
                    {
                        model.DisplayName = "Nährstoffanspruch";
                        model.DisplayValue = ((NutrientClaim)Convert.ToInt32(keyValuePair.Value)).ToString();
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "RootDepth":
                    {
                        model.DisplayName = "Wurzel-Tiefe";
                        model.DisplayValue = ((RootDepth)Convert.ToInt32(keyValuePair.Value)).ToString();
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "Sowing":
                    {
                        model.DisplayName = "Aussaat";
                        model.DisplayValue = ((TimePeriodMonth)Convert.ToInt32(keyValuePair.Value)).ToString();
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "Harvest":
                    {
                        model.DisplayName = "Ernten";
                        model.DisplayValue = ((TimePeriodMonth)Convert.ToInt32(keyValuePair.Value)).ToString();
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "Bloom":
                    {
                        model.DisplayName = "Blütezeit";
                        model.DisplayValue = ((TimePeriodMonth)Convert.ToInt32(keyValuePair.Value)).ToString();
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "SeedMaturity":
                    {
                        model.DisplayName = "Samenreife";
                        model.DisplayValue = ((TimePeriodMonth)Convert.ToInt32(keyValuePair.Value)).ToString();
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "PositivInteractionOn":
                    {
                        string plantName = subjectManager.Get(Convert.ToInt64(keyValuePair.Value))?.Name;

                        model.DisplayName = "Positiven Effekt auf";
                        model.DisplayValue = plantName;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "NegativInteractionOn":
                    {
                        string plantName = subjectManager.Get(Convert.ToInt64(keyValuePair.Value))?.Name;

                        model.DisplayName = "Negativen Effekt auf";
                        model.DisplayValue = plantName;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "DoNegativInteraction":
                    {
                        string plantName = subjectManager.Get(Convert.ToInt64(keyValuePair.Value))?.Name;

                        model.DisplayName = "beeinflusst negativ";
                        model.DisplayValue = plantName;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "DoPositivInteraction":
                    {
                        string plantName = subjectManager.Get(Convert.ToInt64(keyValuePair.Value))?.Name;

                        model.DisplayName = "beeinflusst positiv";
                        model.DisplayValue = plantName;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                default:
                    {
                        model.DisplayName = keyValuePair.Key;
                        model.DisplayValue = keyValuePair.Value;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
            }

            return model;
        }

    }
}