using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<Node> ReadFile(Stream file, string fileName, string entityName)
        {
            List<Node> nodes = new List<Node>();

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
                                nodes.Add(rowToPlant(line));
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
                                nodes.Add(rowToAnimal(line));
                        }

                        break;
                    }
                }
            }


            return nodes;
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

                //set rank
                subject.Rank = TaxonRank.SubSpecies;

                //set Image
                //Media media = new Media();
                //media.ImagePath = "/Images/" + subject.Name + ".jpg";
                //subject.Medias.Add(media);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return subject;

        }

        private void setStructure(string line)
        {
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));
            Structure = values.ToList();
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

                        if(tpss.Count()==1)
                            tp = new Bloom(tpss[0].Trim(), tpss[0].Trim(), type) as T;

                        if(tpss.Count()==2)
                            tp = new Bloom(tpss[0].Trim(), tpss[1].Trim(), type) as T;

                        if (!TimePeriod.IsEmpty(tp)) temp.Add(tp);
                    }
                    
                    if (type.Equals(TimePeriodType.Harvest))
                    {
                        tp = new Harvest() as T;
                        if(tpss.Count()==1)
                            tp = new Harvest(tpss[0].Trim(), tpss[0].Trim(), type) as T;

                        if(tpss.Count()==2)
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

                        if (!TimePeriod.IsEmpty((tp)))temp.Add(tp);
                    }

                }
            }
            return temp;
        }
    }
}
