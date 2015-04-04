using MCT.DB.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.IO
{

    public class AsciiReader:DataReader
    {
        private Stream FileStream;
        private string FileName { get; set; }

        public int StartPosition  { get; set; }

        public AsciiReader()
        {
            Seperator = TextSeperator.tab;
            Decimal = DecimalCharacter.comma;
            //Orientation = IO.Orientation.columnwise;
            StartPosition = 2;
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


            using (StreamReader streamReader = new StreamReader(file))
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
                            if(position>StartPosition)
                                nodes.Add(RowToPlant(line));
                        }

                        break;
                    }
                }
            }


            return nodes;
        }

        /// <summary>
        /// Name	
        /// Species	
        /// Class	
        /// Order	
        /// Family	
        /// Genus	
        /// Description	
        /// Width	
        /// Height	
        /// RootDepth	
        /// SowingDepth	
        /// NutrientClaim	
        /// SowingStart	
        /// SowingEnd	
        /// BloomStart	
        /// BloomEnd	
        /// HarvestStart	
        /// HarvestEnd	
        /// SeedMaturityStart	
        /// SeedMaturityEnd	
        /// CultivationDateStart	
        /// CultivationDateEnd	
        /// GerminationTemperature	
        /// GerminationPeriodDays
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>

        private Node RowToPlant(string line)
        { 
            string[] values = line.Split(IOHelper.GetSeperator(Seperator));

            Plant plant = new Plant();

            try
            {

                plant.Name = values[0];
                plant.ScientificName = values[1];

                // Class    2	
                // Order	3
                // Family	4
                // Genus	5

                plant.Description = values[6];
                plant.Width = Convert.ToInt32(values[7]);
                plant.Height = Convert.ToInt32(values[8]);
                plant.RootDetph = values[9];
                plant.SowingDepth = values[10];
                plant.NutrientClaim = values[11];
                plant.SowingStart = Convert.ToInt32(values[12]);
                plant.SowingEnd = Convert.ToInt32(values[13]);
                plant.BloomStart = Convert.ToInt32(values[14]);
                plant.BloomEnd = Convert.ToInt32(values[15]);
                plant.HarvestStart = Convert.ToInt32(values[16]);
                plant.HarvestEnd = Convert.ToInt32(values[17]);
                plant.SeedMaturityStart = Convert.ToInt32(values[18]);
                plant.SeedMaturityEnd = Convert.ToInt32(values[19]);

                // CultivationDateStart	    20
                // CultivationDateEnd	    21
                // GerminationTemperature	22
                // GerminationPeriodDays    23


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return plant;

        }
    }
}
