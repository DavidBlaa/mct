using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            this.FileStream = file;
            this.FileName = fileName;

            // Check params
            if (this.FileStream == null)
            {
                throw new Exception("File not exist");
            }

            if (!this.FileStream.CanRead)
            {
                throw new Exception("File is not readable");
            }


            using (StreamReader streamReader = new StreamReader(file, System.Text.Encoding.Default))
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
                        case "RootDepth":{ plant.RootDepth = PlantHelper.GetRootDepth(values[9]); break;}
                        case "SowingDepth":{ plant.SowingDepth = Convert.ToInt32(values[i]); break;}
                        case "NutrientClaim":{ plant.NutrientClaim = PlantHelper.GetNutrientClaimDepth(values[i]); break;}
                        case "Sowing": { plant.Sowing = createTimePeriods(values[i]); break;}
                        case "Bloom": { plant.Bloom = createTimePeriods(values[i]); break;}
                        case "Harvest": { plant.Harvest = createTimePeriods(values[i]); break; }
                        case "SeedMaturity": { plant.SeedMaturity = createTimePeriods(values[i]); break; }
                    }
                }

                //set rank
                plant.Rank = TaxonRank.SubSpecies;

                //set Image
                Media media = new Media();
                media.ImagePath = "/Images/" + plant.Name + ".jpg";
                plant.Medias.Add(media);


                //plant.SowingStart = Convert.ToInt32(values[12]);
                //plant.SowingEnd = Convert.ToInt32(values[13]);
                //plant.BloomStart = Convert.ToInt32(values[14]);
                //plant.BloomEnd = Convert.ToInt32(values[15]);
                //plant.HarvestStart = Convert.ToInt32(values[16]);
                //plant.HarvestEnd = Convert.ToInt32(values[17]);
                //plant.SeedMaturityStart = Convert.ToInt32(values[18]);
                //plant.SeedMaturityEnd = Convert.ToInt32(values[19]);

                #region cultivation

                // Entity Cultivation erstellen
                // CultivationDateStart	    20
                // CultivationDateEnd	    21
                // GerminationTemperature	22
                // GerminationPeriodDays    23

                //if(!String.IsNullOrEmpty(values[20])&&
                //   !String.IsNullOrEmpty(values[21])&&
                //   !String.IsNullOrEmpty(values[22])&&
                //   !String.IsNullOrEmpty(values[23]))
                //{
                //    Cultivation cultivation = new Cultivation();
                //    cultivation.CultivationDateStart = Convert.ToInt32(values[20]);
                //    cultivation.CultivationDateEnd = Convert.ToInt32(values[21]);
                //    cultivation.GerminationTemperature = Convert.ToDouble(values[22]);
                //    cultivation.GerminationPeriodDays = Convert.ToInt32(values[23]);

                //    plant.Cultivation = cultivation;
                //}


                #endregion


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return plant;

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
        private ICollection<TimePeriod> createTimePeriods(string value)
        {
            List<TimePeriod> temp = new List<TimePeriod>();
            if (!string.IsNullOrEmpty(value))
            {


                string[] timePeriodStrings = value.Split('#');

                foreach (string tps in timePeriodStrings)
                {
                    string[] tpss = tps.Split('-');

                    TimePeriod tp = new TimePeriod();

                    if(tpss.Count()==1)
                        tp = new TimePeriod(tpss[0].Trim(), tpss[0].Trim());

                    if(tpss.Count()==2)
                        tp = new TimePeriod(tpss[0].Trim(), tpss[1].Trim());

                    if(!TimePeriod.IsEmpty(tp))
                        temp.Add(tp);

                }
            }
            return temp;
        }
    }
}
