using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MCT.IO
{

    public class AsciiReader : DataReader
    {
        private Stream FileStream;
        private string FileName;

        private List<string> Structure;
        private List<AddtionalNameHelper> allAdditionalNameHelpers;

        private SubjectManager subjectManager;
        private WikipediaReader wikipediaReader;

        public int StartPosition { get; set; }

        public AsciiReader()
        {
            subjectManager = new SubjectManager();
            Seperator = TextSeperator.tab;
            Decimal = DecimalCharacter.comma;
            //Orientation = IO.Orientation.columnwise;
            StartPosition = 2;
            Structure = new List<string>();
            allAdditionalNameHelpers = loadAddtionialNames();
            wikipediaReader = new WikipediaReader();
        }

        public List<String> ReadFile(Stream fileStream)
        {
            List<string> lines = new List<string>();

            // Check params
            if (fileStream == null)
            {
                throw new Exception("File not exist");
            }

            if (!fileStream.CanRead)
            {
                throw new Exception("File is not readable");
            }


            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default))
            {
                string line;

                int position = 0;


                while ((line = streamReader.ReadLine()) != null)
                {
                    position++;
                    if (position == 1)
                    {
                        setStructure(line);
                    }

                    if (position >= StartPosition)
                        lines.Add(line);
                }

            }

            return lines;
        }

        public List<String> ReadFile(Stream file, string fileName)
        {
            List<string> lines = new List<string>();

            FileStream = file;
            FileName = fileName;

            // Check params
            if (FileStream == null)
            {
                throw new Exception("File not exist");
            }

            if (!FileStream.CanRead)
            {
                throw new Exception("File is not readable");
            }


            using (StreamReader streamReader = new StreamReader(file, Encoding.Default))
            {
                string line;

                int position = 0;


                while ((line = streamReader.ReadLine()) != null)
                {
                    position++;
                    if (position == 1)
                    {
                        setStructure(line);
                    }

                    if (position >= StartPosition)
                        lines.Add(line);
                }

            }

            return lines;
        }

        public List<T> ReadFile<T>(Stream file, string fileName, string entityName) where T : class
        {
            List<T> nodes = new List<T>();

            FileStream = file;
            FileName = fileName;

            // Check params
            if (FileStream == null)
            {
                throw new Exception("File not exist");
            }

            if (!FileStream.CanRead)
            {
                throw new Exception("File is not readable");
            }


            using (StreamReader streamReader = new StreamReader(file, Encoding.Default))
            {
                string line;

                int position = 0;

                switch (entityName)
                {
                    case "Plant":
                        {

                            Seperator = TextSeperator.tab;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                    nodes.Add(rowToPlant(line) as T);
                            }

                            break;
                        }

                    case "Plant_MKT":
                        {
                            Seperator = TextSeperator.semicolon;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                {
                                    T n = rowToPlant_MKT(line) as T;
                                    if (n != null) nodes.Add(n);
                                }
                            }

                            break;
                        }

                    case "Plant_MKT_UPDATE":
                        {
                            Seperator = TextSeperator.semicolon;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                {
                                    T n = rowToPlant_MKT_UPDATE(line) as T;
                                    if (n != null) nodes.Add(n);
                                }
                            }

                            break;
                        }
                    case "Plant_MKT_UPDATE_INTERACTION":
                        {
                            Seperator = TextSeperator.semicolon;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                {
                                    List<T> n = updateIteractionsFromPlants_MKT_UPDATE(line) as List<T>;
                                    if (n != null) nodes.AddRange(n);
                                }
                            }

                            break;
                        }

                    case "Animal":
                        {
                            Seperator = TextSeperator.tab;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                    nodes.Add(rowToAnimal(line) as T);
                            }

                            break;
                        }

                    case "Predicate":
                        {
                            Seperator = TextSeperator.tab;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                    nodes.Add(rowToPredicate(line, nodes as List<Predicate>) as T);
                            }

                            break;
                        }

                    case "Effect":
                        {
                            Seperator = TextSeperator.tab;

                            while ((line = streamReader.ReadLine()) != null)
                            {
                                position++;
                                if (position == 1)
                                {
                                    setStructure(line);
                                }

                                if (position >= StartPosition)
                                    nodes.Add(rowToEffect(line) as T);
                            }

                            break;
                        }
                }
            }


            return nodes;
        }

        /// <summary>
        /// 0 Name                	
        /// 1 Subspecies 1
        ///     2 Species 2
        ///     3 Class 6
        ///     4 Order	 5
        ///     5 Family 4	
        ///     6 Genus	3
        /// 7 Description
        /// 8 Width
        /// 9 Height	
        /// 10RootDepth	
        /// 11SowingDepth	
        /// 12NutrientClaim	
        /// 13Sowing(Aussaat)	
        /// 14Bloom(Blüte)	
        /// 15Harvest(Ernte)
        /// 16SeedMaturity(Samenreife)	
        /// 17CultivationDate	
        /// 18GerminationTemperature	
        /// 19GerminationPeriodDays
        /// 20 Image
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Node rowToPlant(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            //Debug.WriteLine("values count : "+values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            Plant plant = new Plant();

            try
            {
                for (int i = 0; i < Structure.Count(); i++)
                {
                    string variable = Structure.ElementAt(i);

                    switch (variable)
                    {
                        case "Name": { plant.Name = values[i]; break; }
                        case "Subspecies": { plant.ScientificName = values[i]; break; }
                        case "Description":
                            {

                                string description = values[i];
                                if (description.Length > 250)
                                    plant.Description = description;
                                else
                                    plant.Description = values[i];

                                break;

                            }
                        case "Width": { plant.Width = Convert.ToInt32(values[i]); break; }
                        case "Height": { plant.Height = Convert.ToInt32(values[i]); break; }
                        case "RootDepth": { plant.RootDepth = PlantHelper.GetRootDepth(values[i]); break; }
                        case "SowingDepth": { plant.SowingDepth = Convert.ToInt32(values[i]); break; }
                        case "NutrientClaim": { plant.NutrientClaim = PlantHelper.GetNutrientClaimDepth(values[i]); break; }
                        case "Sowing": { plant.Sowing = createTimePeriods<Sowing>(values[i], TimePeriodType.Sowing); break; }
                        case "Bloom": { plant.Bloom = createTimePeriods<Bloom>(values[i], TimePeriodType.Bloom); break; }
                        case "Harvest":
                            {
                                plant.Harvest = createTimePeriods<Harvest>(values[i], TimePeriodType.Harvest); break;
                            }
                        case "SeedMaturity": { plant.SeedMaturity = createTimePeriods<SeedMaturity>(values[i], TimePeriodType.SeedMaturity); break; }
                        case "Image":
                            {
                                if (!String.IsNullOrEmpty(values[i]))
                                {
                                    Media media = new Media();
                                    media.ImagePath = "/Images/" + values[i];
                                    plant.Medias.Add(media);
                                }
                                break;
                            }
                    }
                }

                //set rank
                plant.Rank = TaxonRank.SubSpecies;

                //     2 Species 2
                //     3 Class 6
                //     4 Order	 5
                //     5 Family 4	
                //     6 Genus	3
                //ToDO check if the plant is realy a sub species
                plant.Parent = generateTaxonParents(values[2], values[6], values[5], values[4], values[3]);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return plant;

        }

        /// <summary>
        /// 0 Name                	
        /// 1 Subspecies
        ///     2 Species	
        ///     3 Class
        ///     4 Order	
        ///     5 Family	
        ///     6 Genus	
        /// 7 Description
        /// 8 Image
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Node rowToAnimal(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            //Debug.WriteLine("values count : " + values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            Animal subject = new Animal();

            try
            {
                for (int i = 0; i < Structure.Count(); i++)
                {
                    string variable = Structure.ElementAt(i);

                    switch (variable)
                    {
                        case "Name": { subject.Name = values[i]; break; }
                        case "Subspecies": { subject.ScientificName = values[i]; break; }
                        case "Description":
                            {

                                string description = values[i];
                                if (description.Length > 250)
                                    subject.Description = description;
                                else
                                    subject.Description = values[i];

                                break;

                            }

                        case "Image":
                            {
                                if (!String.IsNullOrEmpty(values[i]))
                                {
                                    Media media = new Media();
                                    media.ImagePath = "/Images/" + values[i];
                                    subject.Medias.Add(media);
                                }
                                break;
                            }
                    }
                }

                subject.Rank = TaxonRank.SubSpecies;
                subject.Parent = generateTaxonParents(values[2], values[6], values[5], values[4], values[3]);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return subject;

        }

        /// <summary>
        /// 0 Name                	
        /// 1 Description
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Node rowToEffect(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            //Debug.WriteLine("values count : " + values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            Effect subject = new Effect();

            try
            {
                for (int i = 0; i < Structure.Count(); i++)
                {
                    string variable = Structure.ElementAt(i);

                    switch (variable)
                    {
                        case "Name": { subject.Name = values[i]; break; }
                        case "Description":
                            {

                                string description = values[i];
                                if (description.Length > 250)
                                    subject.Description = description;
                                else
                                    subject.Description = values[i];

                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return subject;

        }

        /// <summary>
        /// 1. Name
        /// 2. Description
        /// 3. ParentName
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Predicate rowToPredicate(string line, List<Predicate> predicates)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            //Debug.WriteLine("values count : " + values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            Predicate predicate = new Predicate();

            try
            {
                for (int i = 0; i < Structure.Count(); i++)
                {
                    string variable = Structure.ElementAt(i);

                    switch (variable)
                    {
                        case "Name": { predicate.Name = values[i]; break; }
                        case "Description": { predicate.Description = values[i]; break; }
                        case "Parent":
                            {
                                if (!string.IsNullOrEmpty(values[i]))
                                {
                                    if (predicates.Select(p => p.Name.Equals(values[i])).Any())
                                        predicate.Parent = predicates.FirstOrDefault(p => p.Name.Equals(values[i]));
                                }

                                break;
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return predicate;
        }

        private void setStructure(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));
            Structure = values.ToList();
        }

        public List<Interaction> ConvertToInteractions(List<string> sources, IEnumerable<Subject> subjects, IEnumerable<Predicate> predicates)
        {
            List<Interaction> interactions = new List<Interaction>();

            foreach (var line in sources)
            {
                /*
                 * 1. Subject	
                 * 2. Predicate	
                 * 3. Object	
                 * 4. Indicator	
                 * 5. ImpactSubject
                 * 
                 * */
                string[] values = line.Split(IOHelper.GetSeperator(Seperator));
                string subjectName = values[0];
                string predicateName = values[1];
                string objectName = values[2];
                string indicator = values[3];
                string impactSubjectName = values[4];

                if (!string.IsNullOrEmpty((subjectName)) && subjects.Select(s => s.Name.Equals(subjectName)).Any() &&
                    !string.IsNullOrEmpty((predicateName)) && predicates.Select(p => p.Name.Equals(predicateName)).Any() &&
                    !string.IsNullOrEmpty((objectName)) && subjects.Select(s => s.Name.Equals(objectName)).Any() &&
                    (string.IsNullOrEmpty(impactSubjectName) ||
                     !string.IsNullOrEmpty((impactSubjectName)) && subjects.Select(s => s.Name.Equals(impactSubjectName)).Any()))
                {
                    Subject impactSubject = string.IsNullOrEmpty(impactSubjectName)
                        ? null
                        : subjects.FirstOrDefault(s => s.Name.Equals(impactSubjectName));

                    //string.IsNullOrEmpty(searchValue) ? SearchProvider.Search(searchValue) : SearchProvider.Search(searchValue);
                    interactions.Add(createInterAction(
                        subjects.FirstOrDefault(s => s.Name.Equals(subjectName)),
                        predicates.FirstOrDefault(p => p.Name.Equals(predicateName)),
                        subjects.FirstOrDefault(s => s.Name.Equals(objectName)),
                        Convert.ToInt32(indicator), impactSubject
                        ));

                }
                else
                {
                    if (!subjects.Select(s => s.Name.Equals(subjectName)).Any())
                        Debug.WriteLine(subjectName + " - Subject is missing");

                    if (!predicates.Select(p => p.Name.Equals(predicateName)).Any())
                        Debug.WriteLine(predicateName + " - Predicate is missing");

                    if (!subjects.Select(s => s.Name.Equals(objectName)).Any())
                        Debug.WriteLine(subjectName + " - object is missing");

                    if (!subjects.Select(s => s.Name.Equals(impactSubjectName)).Any())
                        Debug.WriteLine(subjectName + " - ImpactSubject is missing");
                }

            }

            return interactions;

        }

        #region Create Helper

        private Interaction createInterAction(Subject subject, Predicate predicate, Subject _object, int Indicator,
            Subject impactSubject)
        {
            try
            {
                return new Interaction()
                {
                    Subject = subject,
                    Predicate = predicate,
                    Object = _object,
                    Indicator = Indicator,
                    ImpactSubject = impactSubject

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Erzeugt eine Timerperiod aus dem pattern
        /// AAA BBB - AAA BBB # BBB
        /// 
        /// AAA = TimeArea
        /// BBB = Month
        /// </summary>
        /// <param name="value">string pattern vom File</param>
        /// <returns></returns>
        private ICollection<T> createTimePeriods<T>(string value, TimePeriodType type) where T : TimePeriod
        {
            List<T> temp = new List<T>();
            if (!string.IsNullOrEmpty(value))
            {
                string[] timePeriodStrings = value.Split('#');

                T tp;

                foreach (string tps in timePeriodStrings)
                {
                    string[] tpss = tps.Split('-');

                    if (type.Equals(TimePeriodType.Bloom))
                    {
                        tp = new Bloom() as T;

                        if (tpss.Count() == 1)
                            tp = new Bloom(tpss[0].Trim(), tpss[0].Trim(), type) as T;

                        if (tpss.Count() == 2)
                            tp = new Bloom(tpss[0].Trim(), tpss[1].Trim(), type) as T;

                        if (!TimePeriod.IsEmpty(tp)) temp.Add(tp);
                    }

                    if (type.Equals(TimePeriodType.Harvest))
                    {
                        tp = new Harvest() as T;
                        if (tpss.Count() == 1)
                            tp = new Harvest(tpss[0].Trim(), tpss[0].Trim(), type) as T;

                        if (tpss.Count() == 2)
                            tp = new Harvest(tpss[0].Trim(), tpss[1].Trim(), type) as T;

                        if (!TimePeriod.IsEmpty(tp)) temp.Add(tp);
                    }

                    if (type.Equals(TimePeriodType.Sowing))
                    {
                        tp = new Sowing() as T;

                        if (tpss.Count() == 1)
                            tp = new Sowing(tpss[0].Trim(), tpss[0].Trim(), type) as T;

                        if (tpss.Count() == 2)
                            tp = new Sowing(tpss[0].Trim(), tpss[1].Trim(), type) as T;

                        if (!TimePeriod.IsEmpty(tp)) temp.Add(tp);
                    }

                    if (type.Equals(TimePeriodType.SeedMaturity))
                    {
                        tp = new SeedMaturity() as T;

                        if (tpss.Count() == 1)
                            tp = new SeedMaturity(tpss[0].Trim(), tpss[0].Trim(), type) as T;

                        if (tpss.Count() == 2)
                            tp = new SeedMaturity(tpss[0].Trim(), tpss[1].Trim(), type) as T;

                        if (!TimePeriod.IsEmpty((tp))) temp.Add(tp);
                    }

                }
            }
            return temp;
        }

        private Node generateTaxonParents(string species, string genus, string family, string order, string className)
        {
            //todo duplikate checken - in der Datenbank sind die Scientific names mehrfach drin

            SubjectManager subjectManager = new SubjectManager();

            #region class

            Taxon classTaxon = null;

            if (
                subjectManager.GetAll<Taxon>()
                    .Any(p => p.Rank.Equals(TaxonRank.Class) && p.ScientificName.ToLower().Equals(className.ToLower())))
            {
                classTaxon = subjectManager.GetAll<Taxon>().FirstOrDefault(p => p.Rank.Equals(TaxonRank.Class) && p.ScientificName.ToLower().Equals(className.ToLower()));
            }
            else
            {
                List<AddtionalNameHelper> addtionalNameHelperMatches = allAdditionalNameHelpers.Where(p => p.ScientificName.ToLower().Equals(className.ToLower())).ToList();

                string name = className;

                if (addtionalNameHelperMatches.Any())
                    name = addtionalNameHelperMatches.FirstOrDefault().Name;

                classTaxon = new Taxon()
                {
                    Rank = TaxonRank.Class,
                    Name = name,
                    ScientificName = className
                };

                subjectManager.Create<Taxon>(classTaxon);

            }

            #endregion

            #region order

            Taxon orderTaxon = null;

            if (
                subjectManager.GetAll<Taxon>()
                    .Any(p => p.Rank.Equals(TaxonRank.Order) && p.ScientificName.ToLower().Equals(order.ToLower())))
            {
                orderTaxon = subjectManager.GetAll<Taxon>().FirstOrDefault(p => p.Rank.Equals(TaxonRank.Order) && p.ScientificName.ToLower().Equals(order.ToLower()));
            }
            else
            {
                List<AddtionalNameHelper> addtionalNameHelperMatches = allAdditionalNameHelpers.Where(p => p.ScientificName.ToLower().Equals(order.ToLower())).ToList();
                string name = order;

                if (addtionalNameHelperMatches.Any())
                    name = addtionalNameHelperMatches.FirstOrDefault().Name;


                orderTaxon = new Taxon()
                {
                    Rank = TaxonRank.Order,
                    Name = name,
                    ScientificName = order,
                    Parent = classTaxon
                };

                subjectManager.Create<Taxon>(orderTaxon);
            }

            #endregion

            #region family

            Taxon familyTaxon = null;

            if (
                subjectManager.GetAll<Taxon>()
                    .Any(p => p.Rank.Equals(TaxonRank.Family) && p.ScientificName.ToLower().Equals(family.ToLower())))
            {
                familyTaxon = subjectManager.GetAll<Taxon>().FirstOrDefault(p => p.Rank.Equals(TaxonRank.Family) && p.ScientificName.ToLower().Equals(family.ToLower()));
            }
            else
            {
                List<AddtionalNameHelper> addtionalNameHelperMatches = allAdditionalNameHelpers.Where(p => p.ScientificName.ToLower().Equals(family.ToLower())).ToList();

                string name = family;

                if (addtionalNameHelperMatches.Any())
                    name = addtionalNameHelperMatches.FirstOrDefault().Name;

                familyTaxon = new Taxon()
                {
                    Rank = TaxonRank.Family,
                    Name = name,
                    ScientificName = family,
                    Parent = orderTaxon
                };

                subjectManager.Create<Taxon>(familyTaxon);
            }

            #endregion

            #region genus

            Taxon genusTaxon = null;

            if (
                subjectManager.GetAll<Taxon>()
                    .Any(p => p.Rank.Equals(TaxonRank.Genus) && p.ScientificName.ToLower().Equals(genus.ToLower())))
            {
                genusTaxon = subjectManager.GetAll<Taxon>().FirstOrDefault(p => p.Rank.Equals(TaxonRank.Genus) && p.ScientificName.ToLower().Equals(genus.ToLower()));
            }
            else
            {
                List<AddtionalNameHelper> addtionalNameHelperMatches = allAdditionalNameHelpers.Where(p => p.ScientificName.ToLower().Equals(genus.ToLower())).ToList();

                string name = genus;

                if (addtionalNameHelperMatches.Any())
                    name = addtionalNameHelperMatches.FirstOrDefault().Name;

                genusTaxon = new Taxon()
                {
                    Rank = TaxonRank.Genus,
                    Name = name,
                    ScientificName = genus,
                    Parent = familyTaxon
                };

                subjectManager.Create<Taxon>(genusTaxon);
            }

            #endregion

            #region species

            Taxon speciesTaxon = null;

            if (
                subjectManager.GetAll<Taxon>()
                    .Any(p => p.Rank.Equals(TaxonRank.Species) && p.ScientificName.ToLower().Equals(species.ToLower())))
            {
                speciesTaxon = subjectManager.GetAll<Taxon>().FirstOrDefault(p => p.Rank.Equals(TaxonRank.Species) && p.ScientificName.ToLower().Equals(species.ToLower()));
            }
            else
            {
                List<AddtionalNameHelper> addtionalNameHelperMatches = allAdditionalNameHelpers.Where(p => p.ScientificName.ToLower().Equals(species.ToLower())).ToList();

                string name = species;

                if (addtionalNameHelperMatches.Any())
                    name = addtionalNameHelperMatches.FirstOrDefault().Name;

                speciesTaxon = new Taxon()
                {
                    Rank = TaxonRank.Species,
                    Name = name,
                    ScientificName = species,
                    Parent = genusTaxon
                };
            }

            #endregion

            return speciesTaxon;
        }

        #endregion

        #region readAdditionalNameFile

        private List<AddtionalNameHelper> loadAddtionialNames()
        {
            List<AddtionalNameHelper> temp = new List<AddtionalNameHelper>();

            string path = Path.Combine(AppConfigHelper.GetWorkspace(), "TaxonNamesSeedData.txt");

            Stream fileStream = Open(path);

            List<string> rows = ReadFile(fileStream);

            foreach (string row in rows)
            {
                temp.Add(rowToAddtionalNameHelper(row));
            }

            return temp;
        }

        private AddtionalNameHelper rowToAddtionalNameHelper(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            Debug.WriteLine("values count : " + values.Count());
            Debug.WriteLine("datastructure count : " + Structure.Count());

            AddtionalNameHelper subject = new AddtionalNameHelper();

            try
            {
                for (int i = 0; i < Structure.Count(); i++)
                {
                    string variable = Structure.ElementAt(i);

                    switch (variable)
                    {
                        case "Name": { subject.Name = values[i]; break; }
                        case "ScientificName": { subject.ScientificName = values[i]; break; }
                        case "Sprache": { subject.Language = values[i]; break; }
                        case "BevorzugterName": { subject.IsPreferredName = Convert.ToBoolean(values[i]); break; }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return subject;

        }

        #endregion

        #region read MischkulturTabelle.txt

        /// <summary>
        /// 0 Pflanze                	
        /// 1 Pflanzenfamilie
        /// 2 günstige Partner
        /// 3 ungünstige Partner
        /// 4 Vorkultur / Nachkultur
        /// 5     1. Aussaattiefe in cm /
        //2. Keimtemperatur
        //(optimal/minimum) in °C /
        //3. Keimdauer in Tagen /
        //4. Keimfähigkeit der Samen in Jahren
        /// 6 Bemerkungen
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Node rowToPlant_MKT(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            //Debug.WriteLine("values count : " + values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            if (values.Count() == Structure.Count)
            {
                Plant plant = new Plant();

                try
                {
                    for (int i = 0; i < Structure.Count(); i++)
                    {
                        string variable = Structure.ElementAt(i);
                        //Debug.WriteLine(variable);
                        switch (variable)
                        {
                            case "Pflanze":
                                {
                                    plant.Name = GetFirstNameMKT(values[i]);
                                    plant.ScientificName = GetScientificNameMKT(values[i], plant.Name);
                                    //plant.Parent = saveParents(plant.ScientificName, typeof(Plant));
                                    break;

                                }
                            case "1. Aussaattiefe in cm /2. Keimtemperatur(optimal/minimum) in °C /3. Keimdauer in Tagen /4. Keimfähigkeit der Samen in Jahren": { break; }
                            case "Bemerkungen":
                                {
                                    plant = UpdatePlantBasedOnDescription(plant, values[i]);
                                    string description = values[i];
                                    break;
                                }
                        }
                    }

                    // find parents
                    // get genus
                    string genusScientifcName = plant.ScientificName.Split(' ').FirstOrDefault();

                    //Get or create genus
                    if (!String.IsNullOrEmpty(genusScientifcName)) plant.Parent = GetOrCreate(genusScientifcName, TaxonRank.Genus);

                    //set rank
                    plant.Rank = TaxonRank.Species;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                if (string.IsNullOrEmpty(plant.Name) && string.IsNullOrEmpty(plant.ScientificName))
                {
                    Debug.WriteLine("no names not stored  :" + values);
                    return null;
                }

                return plant;
            }
            else
            {
                Debug.WriteLine("values error in number of vars  :" + values);
                return null;
            }



        }



        /// <summary>
        /// Zweite Runde zum einlesen der Planzen aus der 
        /// Mischkultur Tabelle. 
        /// Da Assosciations gespeichert werden müssen,
        /// wurde der erste Durchlauf zum erstellen der Planzen  genutzt
        /// und der zweite druchlauf zum erstellenn der Verbindungen
        /// 
        /// 0 Pflanze                	
        /// 1 Pflanzenfamilie
        /// 2 günstige Partner
        /// 3 ungünstige Partner
        /// 4 Vorkultur / Nachkultur
        /// 5     1. Aussaattiefe in cm /
        //2. Keimtemperatur
        //(optimal/minimum) in °C /
        //3. Keimdauer in Tagen /
        //4. Keimfähigkeit der Samen in Jahren
        /// 6 Bemerkungen
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Node rowToPlant_MKT_UPDATE(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            //Debug.WriteLine("values count : " + values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            if (values.Count() == Structure.Count)
            {
                int plantNameIndex = Structure.IndexOf(Structure.Where(e => e.Equals("Pflanze")).FirstOrDefault());

                Plant plant = subjectManager.GetAll<Plant>().Where(p => p.Name.Equals(GetFirstNameMKT(values.ElementAt(plantNameIndex)))).FirstOrDefault();

                if (plant == null)
                {
                    Debug.WriteLine("no plant existing for this update:" + values);
                    return null;
                }

                try
                {
                    for (int i = 0; i < Structure.Count(); i++)
                    {
                        string variable = Structure.ElementAt(i);
                        //Debug.WriteLine(variable);
                        switch (variable)
                        {

                            case "Pflanzenfamilie": { break; }
                            case "günstige Partner": { break; }
                            case "ungünstige Partner": { break; }
                            case "Vorkultur / Nachkultur":
                                {

                                    string[] temp = values[i].Split('/');
                                    if (temp.Length > 0)
                                    {
                                        if (!String.IsNullOrEmpty(temp[0]))
                                            SetCultures(plant, "preculture", temp[0]);
                                        if ((temp.Length == 2 || temp.Length > 2) && !String.IsNullOrEmpty(temp[1]))
                                            SetCultures(plant, "afterculture", temp[1]);

                                    }


                                    break;
                                }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }



                if (string.IsNullOrEmpty(plant.Name) && string.IsNullOrEmpty(plant.ScientificName))
                {
                    Debug.WriteLine("no names not stored  :" + values);
                    return null;
                }

                return plant;
            }
            else
            {
                Debug.WriteLine("values error in number of vars  :" + values);
                return null;
            }



        }

        /// <summary>
        /// Dritte Runde zum einlesen der Planzen aus der 
        /// Mischkultur Tabelle. 
        /// Hier werden die interactions erstellt.
        /// 
        /// 0 Pflanze                	
        /// 1 Pflanzenfamilie
        /// 2 günstige Partner
        /// 3 ungünstige Partner
        /// 4 Vorkultur / Nachkultur
        /// 5     1. Aussaattiefe in cm /
        //2. Keimtemperatur
        //(optimal/minimum) in °C /
        //3. Keimdauer in Tagen /
        //4. Keimfähigkeit der Samen in Jahren
        /// 6 Bemerkungen
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private List<Interaction> updateIteractionsFromPlants_MKT_UPDATE(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));
            List<Interaction> tmp = new List<Interaction>();
            //Debug.WriteLine("values count : " + values.Count());
            //Debug.WriteLine("datastructure count : " + Structure.Count());

            if (values.Count() == Structure.Count)
            {
                int plantNameIndex = Structure.IndexOf(Structure.Where(e => e.Equals("Pflanze")).FirstOrDefault());
                int goodIndex = Structure.IndexOf(Structure.Where(e => e.Equals("\"günstige Partner\"")).FirstOrDefault());
                int badIndex = Structure.IndexOf(Structure.Where(e => e.Equals("\"ungünstige Partner\"")).FirstOrDefault());

                Plant plant = subjectManager.GetAll<Plant>().Where(p => p.Name.Equals(GetFirstNameMKT(values.ElementAt(plantNameIndex)))).FirstOrDefault();

                if (plant == null)
                {
                    Debug.WriteLine("no plant existing for this update:" + values);
                    return null;
                }

                try
                {
                    for (int i = 0; i < Structure.Count(); i++)
                    {
                        string variable = Structure.ElementAt(i);
                        Debug.WriteLine("***********************************");
                        Debug.WriteLine("plant for interaction:" + plant.Name);

                        string goods = values.ElementAt(goodIndex);
                        string bads = values.ElementAt(badIndex);
                        switch (variable)
                        {
                            case "\"günstige Partner\"":
                                {
                                    Predicate p = GetOrCreatePredicate("begünstigt", "positiv");
                                    if (!string.IsNullOrEmpty(goods))
                                    {
                                        goods = goods.Replace("\"", "");
                                        foreach (string s in goods.Split(','))
                                        {
                                            Plant subject = GetOrCreatePlant(s.Trim());
                                            tmp.Add(new Interaction()
                                            {
                                                Subject = subject,
                                                Predicate = p,
                                                Object = plant
                                            });
                                        }
                                    }


                                    Debug.WriteLine("günstige Partner :" + goods);
                                    break;
                                }
                            case "\"ungünstige Partner\"":
                                {
                                    Predicate p = GetOrCreatePredicate("schädigt", "negativ");


                                    if (!string.IsNullOrEmpty(bads))
                                    {
                                        bads = bads.Replace("\"", "");

                                        foreach (string s in bads.Split(','))
                                        {
                                            Plant subject = GetOrCreatePlant(s.Trim());
                                            tmp.Add(new Interaction()
                                            {
                                                Subject = subject,
                                                Predicate = p,
                                                Object = plant
                                            });
                                        }
                                    }

                                    Debug.WriteLine("ungünstige Partner :" + bads);
                                    break;
                                }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }



                if (string.IsNullOrEmpty(plant.Name) && string.IsNullOrEmpty(plant.ScientificName))
                {
                    Debug.WriteLine("no names not stored  :" + values);
                    return null;
                }

                return tmp;
            }
            else
            {
                Debug.WriteLine("values error in number of vars  :" + values);
                return null;
            }
        }

        private string GetFirstNameMKT(string value)
        {
            string[] seperateNames = value.Split(',');

            //nur ein name
            if (seperateNames.Length == 1)
            {
                return seperateNames[0].Split('(')[0].Replace('"', ' ').Trim();
            }
            // mehrere name, nur der erste zurückgesendet
            else
            {
                return seperateNames[0].Replace('"', ' ').Trim();
            }

        }

        private string GetScientificNameMKT(string value, string name)
        {
            if (value.Contains("("))
            {

                string[] seperateNames = value.Split('(');
                string lastPart = seperateNames[seperateNames.Length - 1];

                string[] seperateNames2 = lastPart.Split(')');
                string scientificName = seperateNames2[0];

                if (scientificName.Contains("...") || scientificName.Contains('-'))
                    return wikipediaReader.GetScientificName(name);

                return scientificName.Replace('"', ' ').Trim();

            }
            else
            {
                return wikipediaReader.GetScientificName(name);
            }

        }

        private Node saveParents(string scientificName, Type type)
        {
            string[] nameArray = scientificName.Split(' ');
            SubjectManager subjectManager = new SubjectManager();
            WikipediaReader wReader = new WikipediaReader();

            // scientific Name = abc dfg ghj -> subspecies 
            if (nameArray.Count() > 1)
            {
                //create genius
                string geniusScientifcName = nameArray[0];
                Species genius = new Species();
                if (subjectManager.GetAll<Species>().Any(s => s.ScientificName.Equals(geniusScientifcName)))
                {
                    genius = subjectManager.GetAll<Species>().FirstOrDefault(s => s.ScientificName.Equals(geniusScientifcName));
                }
                else
                {
                    genius.ScientificName = geniusScientifcName;
                    genius.Name = wReader.GetName(geniusScientifcName);
                    genius.Rank = TaxonRank.Genus;

                    genius = subjectManager.Create(genius);
                }


                //is subspecies
                if (nameArray.Count() > 2)
                {
                    Species species = new Species();

                    if (type.Equals(typeof(Plant)))
                    {
                        species = new Plant();

                        string speciesScientifcName = nameArray[0] + " " + nameArray[1];

                        if (subjectManager.GetAll<Species>().Any(s => s.ScientificName.Equals(speciesScientifcName)))
                        {
                            species = subjectManager.GetAll<Plant>().FirstOrDefault(s => s.ScientificName.Equals(speciesScientifcName));
                        }
                        else
                        {
                            species.ScientificName = speciesScientifcName;
                            species.Name = wReader.GetName(speciesScientifcName);
                            species.Parent = genius;
                            species.Rank = TaxonRank.Species;
                            species = subjectManager.Create(species);
                        }
                    }
                    else if (type.Equals(typeof(Animal)))
                    {
                        species = new Animal();
                        string speciesScientifcName = nameArray[0] + " " + nameArray[1];

                        if (subjectManager.GetAll<Species>().Any(s => s.ScientificName.Equals(speciesScientifcName)))
                        {
                            species = subjectManager.GetAll<Animal>().FirstOrDefault(s => s.ScientificName.Equals(speciesScientifcName));
                        }
                        else
                        {
                            species.ScientificName = speciesScientifcName;
                            species.Name = wReader.GetName(speciesScientifcName);
                            species.Parent = genius;
                            species.Rank = TaxonRank.Species;
                            species = subjectManager.Create(species);
                        }
                    }

                    return species;
                }


                return genius;
            }

            return null;
        }

        private Plant UpdatePlantBasedOnDescription(Plant plant, string description)
        {

            #region wurzeln

            if (description.Contains("Flachwurzler"))
                plant.RootDepth = RootDepth.Flat;

            if (description.Contains("Mittelwurzler"))
                plant.RootDepth = RootDepth.Flat;

            if (description.Contains("Tiefwurzler"))
                plant.RootDepth = RootDepth.Deep;

            #endregion

            #region Nährstoff

            if (description.Contains("Schwachzehrer"))
                plant.NutrientClaim = NutrientClaim.Weak;

            if (description.Contains("Mittelzehrer"))
                plant.NutrientClaim = NutrientClaim.Medium;

            if (description.Contains("Starkzehrer"))
                plant.NutrientClaim = NutrientClaim.Strong;

            #endregion

            return plant;
        }

        private void SetCultures(Plant plant, string cultureType, string culturesValues)
        {
            List<Plant> cultures = new List<Plant>();


            // get plants - split incoming string
            string[] names = culturesValues.Split(',');

            foreach (string name in names)
            {
                string processedName = name.Split('(')[0].Replace("\"", "").Trim();

                if (subjectManager.GetAll<Plant>().Any(p => p.Name.Equals(processedName)))
                    cultures.Add(subjectManager.GetAll<Plant>().Where(p => p.Name.Equals(processedName)).FirstOrDefault());
                else
                {

                    Debug.WriteLine("create a new plant in SetCultures: " + processedName);
                    string scientificName = wikipediaReader.GetScientificName(processedName);
                    if (!string.IsNullOrEmpty(scientificName))
                    {
                        cultures.Add(new Plant()
                        {
                            Name = processedName,
                            ScientificName = scientificName
                        });
                    }
                }
            }


            if (cultureType.Equals("preculture")) { plant.PreCultures = cultures; }
            if (cultureType.Equals("afterculture")) { plant.AfterCultures = cultures; }

        }

        private Predicate GetOrCreatePredicate(string name, string parentName)
        {
            Predicate tmp = null;

            Predicate parentPredicate = subjectManager.GetAll<Predicate>().Where(p => p.Name.Equals(parentName)).FirstOrDefault();

            if (!subjectManager.GetAll<Predicate>().Any(p => p.Name.Equals(name)))
            {
                tmp = new Predicate();
                tmp.Name = name;
                if (parentPredicate != null)
                    tmp.Parent = parentPredicate;
                tmp = subjectManager.Create(tmp);
            }
            else
            {
                tmp = subjectManager.GetAll<Predicate>().Where(p => p.Name.Equals(name)).FirstOrDefault();
            }

            return tmp;
        }

        private Plant GetOrCreatePlant(string name)
        {
            Plant tmp = null;


            if (!subjectManager.GetAll<Plant>().Any(p => p.Name.Equals(name)))
            {
                tmp = new Plant();
                tmp.Name = name;
                tmp.ScientificName = wikipediaReader.GetScientificName(name);
                tmp = subjectManager.Create(tmp);
            }
            else
            {
                tmp = subjectManager.GetAll<Plant>().Where(p => p.Name.Equals(name)).FirstOrDefault();
            }

            return tmp;
        }

        private Taxon GetOrCreate(string scientificName, TaxonRank rank)
        {
            var p = subjectManager.GetAll<Node>().Where(n => n.ScientificName.Equals(scientificName)).FirstOrDefault();

            if (p != null) return p as Taxon;

            Taxon t = new Taxon();
            t.Name = wikipediaReader.GetName(scientificName);
            t.ScientificName = scientificName;
            t.Rank = rank;

            t = subjectManager.Create(t);

            return t;
        }

        #endregion

    }

    public class AddtionalNameHelper
    {
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public string Language { get; set; }
        public bool IsPreferredName { get; set; }


        public AdditionalName convertToAdditonalName(Node node)
        {
            return new AdditionalName()
            {
                Name = this.Name,
                Language = this.Language,
                Node = node,
                IsPreferredName = IsPreferredName
            };
        }
    }

}
