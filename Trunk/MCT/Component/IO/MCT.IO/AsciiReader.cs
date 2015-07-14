using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MCT.DB.Entities;

namespace MCT.IO
{

    public class AsciiReader:DataReader
    {
        private Stream FileStream;
        private string FileName;

        private List<string> Structure;

        public int StartPosition  { get; set; }

        public AsciiReader()
        {
            Seperator = TextSeperator.tab;
            Decimal = DecimalCharacter.comma;
            //Orientation = IO.Orientation.columnwise;
            StartPosition = 2;
            Structure = new List<string>();
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
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            position++;
                            if(position == 1)
                            {
                                setStructure(line);
                            }

                            if(position>=StartPosition)
                                nodes.Add(rowToPlant(line) as T);
                        }

                        break;
                    }

                    case "Animal":
                    {
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

        /// <summary>
        /// 0 Name                	
        /// 1 Subspecies
        ///     2 Species	
        ///     3 Class
        ///     4 Order	
        ///     5 Family	
        ///     6 Genus	
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

            Debug.WriteLine("values count : "+values.Count());
            Debug.WriteLine("datastructure count : " + Structure.Count());

            Plant plant = new Plant();

            try
            {
                for(int i= 0; i<Structure.Count();i++)
                {
                    string variable = Structure.ElementAt(i);

                    switch(variable)
                    {
                        case "Name":{ plant.Name = values[i]; break;}
                        case "Subspecies":{ plant.ScientificName = values[i]; break;}
                        case "Description":{

                            string description = values[i];
                            if (description.Length > 250)
                                plant.Description = description.Substring(0, 250) + "...";
                            else
                                plant.Description = values[i]; 
                            
                            break;
                        
                        }
                        case "Width":{  plant.Width = Convert.ToInt32(values[i]); break;}
                        case "Height":{ plant.Height = Convert.ToInt32(values[i]); break;}
                        case "RootDepth":{ plant.RootDepth = PlantHelper.GetRootDepth(values[i]); break;}
                        case "SowingDepth":{ plant.SowingDepth = Convert.ToInt32(values[i]); break;}
                        case "NutrientClaim":{ plant.NutrientClaim = PlantHelper.GetNutrientClaimDepth(values[i]); break;}
                        case "Sowing": { plant.Sowing = createTimePeriods<Sowing>(values[i], TimePeriodType.Sowing); break;}
                        case "Bloom": { plant.Bloom = createTimePeriods<Bloom>(values[i], TimePeriodType.Bloom); break; }
                        case "Harvest": { 
                            plant.Harvest = createTimePeriods<Harvest>(values[i], TimePeriodType.Harvest); break; }
                        case "SeedMaturity": { plant.SeedMaturity = createTimePeriods<SeedMaturity>(values[i], TimePeriodType.SeedMaturity); break; }
                        case "Image": {
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

            Debug.WriteLine("values count : " + values.Count());
            Debug.WriteLine("datastructure count : " + Structure.Count());

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
                                subject.Description = description.Substring(0, 250) + "...";
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

            Debug.WriteLine("values count : " + values.Count());
            Debug.WriteLine("datastructure count : " + Structure.Count());

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
                                    subject.Description = description.Substring(0, 250) + "...";
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
        private Predicate rowToPredicate(string line, List<Predicate> predicates )
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            Debug.WriteLine("values count : " + values.Count());
            Debug.WriteLine("datastructure count : " + Structure.Count());

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
                                if (predicates.Select(p=>p.Name.Equals(values[i])).Any())
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


        #endregion

        
    }
}
