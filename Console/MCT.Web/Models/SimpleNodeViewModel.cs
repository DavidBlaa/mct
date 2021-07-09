using MCT.DB.Entities;
using MCT.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class SimpleNodeViewModel
    {
        public long Id { get; set; }

        public String Name { get; set; }

        public String ScientificName { get; set; }

        public String Description { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public TaxonRank TaxonRank { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SubjectType Type { get; set; }

        public SimpleNodeViewModel Parent { get; set; }

        public static SimpleNodeViewModel Convert(Node node)
        {
            SimpleNodeViewModel model = new SimpleNodeViewModel();
            model.Id = node.Id;
            model.Name = node.Name;
            model.ScientificName = node.ScientificName;
            model.Description = node.Description;
            model.TaxonRank = node.Rank;

            model.Type = ModelHelper.GetType(node);

            if (node.Parent != null)
            {
                model.Parent = Convert(node.Parent);
            }

            return model;
        }
    }
}