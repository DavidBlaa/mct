﻿using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Extern;
using System;
using System.Linq;

namespace MCT.Utils
{
    public class Utility
    {
        public static TaxonRank GetTaxonRank(string scientificName)
        {

            /**
             * Lactuca                      -> Genus
             * Lactuca sativa               -> Species 
             * Lactuca sativa var. capitata -> Species 
             * Lactuca sativa var. capitata Larissa -> SubSpecies
             * Lactuca sativa Larissa -> SubSpecies
             * */

            string[] tmp = scientificName.Split(' ');

            //Lactuca sativa               -> Species
            if (tmp.Length == 2) return TaxonRank.Species;

            if (tmp.Length == 3) return TaxonRank.SubSpecies;

            if (tmp.Length == 4) return TaxonRank.SubSpecies;

            return TaxonRank.SubSpecies;
        }

        public static Node CreateOrSetParents(string scientificNameOfSpecies, Type type, SubjectManager subjectManager)
        {
            string[] nameArray = scientificNameOfSpecies.Split(' ');
            WikipediaReader wReader = new WikipediaReader();

            /**
             * Lactuca                      -> Genus
             * Lactuca sativa               -> Species 
             * Lactuca sativa var. capitata -> Variation - Subspecies 
             * Lactuca sativa var. capitata Larissa -> SubSpecies
             * Lactuca sativa Larissa -> SubSpecies
             * */

            //Lactuca sativa - create genus return genus
            Taxon genus = GetOrCreateGenus(nameArray[0], subjectManager);
            if (nameArray.Count() == 2)
            {
                return genus;
            }

            //Lactuca sativa Larissa -> SubSpecies
            if (nameArray.Count() == 3 && !nameArray.Contains("var."))
            {
                string name = nameArray[0] + " " + nameArray[1];
                return GetOrCreateSpecies(name, type, subjectManager, genus);
            }

            //Lactuca sativa var. capitata -> Species 

            if (nameArray.Count() == 4 && nameArray.Contains("var."))
            {
                string name = nameArray[0] + " " + nameArray[1];
                return GetOrCreateSpecies(name, type, subjectManager, genus);

            }

            //Lactuca sativa var. capitata Larissa -> SubSpecies
            if (nameArray.Count() == 5 && nameArray.Contains("var."))
            {
                string name = nameArray[0] + " " + nameArray[1];
                return GetOrCreateSpecies(name, type, subjectManager, genus);
            }

            return null;
        }


        private static Taxon GetOrCreateGenus(string genusName, SubjectManager subjectManager)
        {
            WikipediaReader wReader = new WikipediaReader();

            Taxon genus = new Taxon();
            if (subjectManager.GetAll<Taxon>().Any(s => s.ScientificName.Equals(genusName)))
            {
                genus = subjectManager.GetAll<Taxon>().FirstOrDefault(s => s.ScientificName.Equals(genusName));

                if (string.IsNullOrEmpty(genus.Name) && !string.IsNullOrEmpty(genus.ScientificName))
                {
                    genus.Name = wReader.GetName(genus.ScientificName);
                }

                if (string.IsNullOrEmpty(genus.ScientificName) && !string.IsNullOrEmpty(genus.Name))
                {
                    genus.ScientificName = wReader.GetScientificName(genus.Name);
                }

            }
            else
            {
                genus.ScientificName = genusName;
                genus.Name = wReader.GetName(genusName);

                if (String.IsNullOrEmpty(genus.Name)) genus.Name = genusName;

                genus.Rank = TaxonRank.Genus;

                subjectManager.Create(genus);
            }

            return genus;
        }

        private static Species GetOrCreateSpecies(string speciesName, Type type, SubjectManager subjectManager, Taxon genus)
        {

            WikipediaReader wReader = new WikipediaReader();

            Species species = new Species();

            if (type.Equals(typeof(Plant)))
            {
                species = new Plant();

                if (subjectManager.GetAll<Species>().Any(s => s.ScientificName.Equals(speciesName)))
                {
                    species = subjectManager.GetAll<Plant>().FirstOrDefault(s => s.ScientificName.Equals(speciesName));
                }
                else
                {
                    species.ScientificName = speciesName;
                    species.Name = wReader.GetName(speciesName);
                    species.Parent = genus;
                    species.Rank = TaxonRank.Species;
                    subjectManager.Create(species);
                }
            }
            else if (type.Equals(typeof(Animal)))
            {
                species = new Animal();

                if (subjectManager.GetAll<Species>().Any(s => s.ScientificName.Equals(speciesName)))
                {
                    species = subjectManager.GetAll<Animal>().FirstOrDefault(s => s.ScientificName.Equals(speciesName));
                }
                else
                {
                    species.ScientificName = speciesName;
                    species.Name = wReader.GetName(speciesName);
                    species.Parent = genus;
                    species.Rank = TaxonRank.Species;
                    subjectManager.Create(species);
                }
            }

            return species;
        }
    }
}