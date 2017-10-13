using Lucene.Net.Support;
using MCT.DB.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Models
{
    public class NodeModel : SubjectModel
    {
        public TaxonRank TaxonRank { get; set; }
        [Required]
        [Remote("CheckScientificNameExist", "Subject", ErrorMessage = "Scientific Name existiert bereits.", AdditionalFields = "initScientificName")]
        public String ScientificName { get; set; }
        //public SpeciesType Type { get; set; }

        public NodeModel()
        {
            ScientificName = "";
            TaxonRank = TaxonRank.Species;
            Interactions = new EquatableList<InteractionModel>();
            TimePeriods = new EquatableList<TimePeriodModel>();
        }

        public static NodeModel Convert(Node node)
        {
            NodeModel model = new NodeModel();

            model.Id = node.Id;
            model.ScientificName = node.ScientificName;
            model.TaxonRank = node.Rank;
            if (!String.IsNullOrEmpty(node.Name))
                model.Name = node.Name;

            if (!String.IsNullOrEmpty(node.Description))
                model.Description = node.Description;

            model.Type = GetType(node);

            if (!node.Medias.Any())
            {
                model.ImagePath = "/Images/Empty.png";
            }
            else
            {
                model.ImagePath = node.Medias.First().ImagePath;
            }

            return model;
        }


    }

    public enum SpeciesType
    {
        Animal,
        Plant,
        Unknow
    }
}