using System;

namespace MCT.DB.Entities
{
    public class Node : Subject
    {
        public virtual String ScientificName { get; set; }
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
        SubSpecies,
        Species,
        Genus,
        Family,
        Order,
        Class
    }
}
