using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MCT.DB.Entities
{
    public class Node : Subject
    {
        public virtual String ScientificName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TaxonRank Rank { get; set; }
        public virtual Node Parent { get; set; }
    }

    //Reich         	Regnum          	Vielzellige Tiere
    //Abteilung/Stamm	Divisio/Phylum	    Chordatiere
    //Unterstamm	    Subphylum	        Wirbeltiere
    //Klasse	        Classis	            Säugetiere
    //Ordnung	        Ordo	            Raubtiere
    //Überfamilie	    Superfamilia	    Katzenartige
    //Familie	        Familia	            Katzen
    //Unterfamilie	    Subfamilia	        Kleinkatzen
    //Gattung	        Genus	            Altwelt-Wildkatzen
    //Art	            Species	            Wildkatze
    //Unterart	        Subspecies	        Hauskatze
    public enum TaxonRank
    {
        [Display(Name = "Unterart")]
        [EnumMember(Value = "Unterart")]
        SubSpecies,

        [Display(Name = "Art")]
        [EnumMember(Value = "Art")]
        Species,

        [Display(Name = "Gattung")]
        [EnumMember(Value = "Gattung")]
        Genus,

        [Display(Name = "Familie")]
        [EnumMember(Value = "Familie")]
        Family,

        [Display(Name = "Ordnung")]
        [EnumMember(Value = "Ordnung")]
        Order,

        [Display(Name = "Klasse")]
        [EnumMember(Value = "Klasse")]
        Class
    }
}