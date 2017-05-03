using MCT.DB.Entities;
using System;

namespace MCT.Web.Models
{
    public class SimpleNodeViewModel
    {
        public String Name { get; set; }
        public String ScientificName { get; set; }
        public String Description { get; set; }
        public string TaxonRank { get; set; }
        public SimpleNodeViewModel Parent { get; set; }

        public static SimpleNodeViewModel Convert(Node node)
        {
            SimpleNodeViewModel model = new SimpleNodeViewModel();

            model.Name = node.Name;
            model.ScientificName = node.ScientificName;
            model.Description = node.Description;
            model.TaxonRank = node.Rank.ToString();

            if (node.Parent != null)
            {
                model.Parent = Convert(node.Parent);

            }

            return model;
        }
    }
}