using MCT.DB.Entities;
using MCT.DB.Entities.PatchPlaner;
using MCT.DB.Services;
using MCT.Helpers;
using MCT.Utils;
using MCT.Web.Models;
using MCT.Web.Models.Search;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
                        NutrientClaim nc = (NutrientClaim)Convert.ToInt32(keyValuePair.Value);

                        model.DisplayName = "Nährstoffanspruch";
                        model.DisplayValue = nc.GetAttribute<DisplayAttribute>().Name;
                        model.Key = keyValuePair.Key;
                        model.Value = keyValuePair.Value;
                        break;
                    }
                case "RootDepths":
                    {
                        RootDepth rd = (RootDepth)Convert.ToInt32(keyValuePair.Value);
                        model.DisplayName = "Wurzel-Tiefe";
                        model.DisplayValue = rd.GetAttribute<DisplayAttribute>().Name;
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

        /// <summary>
        /// Get a dictionary with keys and count from the system
        /// Plants, Animals, Interactions, Beete, User
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetStatistics()
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            SubjectManager subjectManager = new SubjectManager();
            InteractionManager interactionManager = new InteractionManager();
            PatchManager patchManager = new PatchManager();

            int plantsCount = subjectManager.GetAll<Plant>().Length;
            int animalsCount = subjectManager.GetAll<Animal>().Length;
            int actionsCount = interactionManager.GetAll<Interaction>().Length;
            int patchesCount = patchManager.GetAll<Patch>().Length;
            int usersCount = 0;

            tmp.Add("Pflanzen", plantsCount);
            tmp.Add("Tiere", animalsCount);
            tmp.Add("Interaktionen", actionsCount);
            tmp.Add("Gärten", patchesCount);
            tmp.Add("Nutzer", usersCount);

            return tmp;
        }

        public static List<SimpleNodeViewModel> GetChildren(long id)
        {
            List<SimpleNodeViewModel> tmp = new List<SimpleNodeViewModel>();
            SubjectManager subjectManager = new SubjectManager();

            var children = subjectManager.GetAllAsQueryable<Node>().Where(n => n.Parent != null && n.Parent.Id.Equals(id));

            children.ToList().ForEach(c => tmp.Add(SimpleNodeViewModel.Convert(c)));

            return tmp;

        }

        public static SubjectType GetType(Subject subject)
        {
            if (subject.IsProxy())
            {
                subject = NHibernateHelper.UnProxyObjectAs<Subject>(subject);
            }

            if ((subject as Plant) != null) return SubjectType.Plant;
            if ((subject as Animal) != null) return SubjectType.Animal;
            if ((subject as Effect) != null) return SubjectType.Effect;
            if ((subject as Taxon) != null) return SubjectType.Taxon;

            return SubjectType.Unknow;
        }
    }
}