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
                            if(position>=StartPosition)
                                nodes.Add(RowToPlant(line));
                        }

                        break;
                    }
                }
            }


            return nodes;
        }

        /// <summary>
        /// Name                0	
        /// Species	            1
        /// Class	            2
        /// Order	            3
        /// Family	            4
        /// Genus	            5
        /// Description	        6
        /// Width	            7
        /// Height	            8
        /// RootDepth	        9
        /// SowingDepth	        10
        /// NutrientClaim       11	
        /// SowingStart	        12
        /// SowingEnd	        13
        /// BloomStart	        14
        /// BloomEnd	        15
        /// HarvestStart	    16
        /// HarvestEnd	        17
        /// SeedMaturityStart	18
        /// SeedMaturityEnd	    19
        /// CultivationDateStart	20
        /// CultivationDateEnd	    21
        /// GerminationTemperature	22
        /// GerminationPeriodDays   23
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

                // Entities müssen geprüft werden
                // wenn vorhanden dann auswählen, ansonsten erstellen
                // Class    2	
                // Order	3
                // Family	4
                // Genus	5

                plant.Description = values[6];
                plant.Width = Convert.ToInt32(values[7]);
                plant.Height = Convert.ToInt32(values[8]);

                // get enum from RootDepth
                plant.RootDepth = PlantHelper.GetRootDepth(values[9]);

                plant.SowingDepth = Convert.ToInt32(values[10]);

                // get enum from NutrientClaim
                plant.NutrientClaim = PlantHelper.GetNutrientClaimDepth(values[11]);

                plant.SowingStart = Convert.ToInt32(values[12]);
                plant.SowingEnd = Convert.ToInt32(values[13]);
                plant.BloomStart = Convert.ToInt32(values[14]);
                plant.BloomEnd = Convert.ToInt32(values[15]);
                plant.HarvestStart = Convert.ToInt32(values[16]);
                plant.HarvestEnd = Convert.ToInt32(values[17]);
                plant.SeedMaturityStart = Convert.ToInt32(values[18]);
                plant.SeedMaturityEnd = Convert.ToInt32(values[19]);

                #region cultivation

                // Entity Cultivation erstellen
                // CultivationDateStart	    20
                // CultivationDateEnd	    21
                // GerminationTemperature	22
                // GerminationPeriodDays    23

                if(!String.IsNullOrEmpty(values[20])&&
                   !String.IsNullOrEmpty(values[21])&&
                   !String.IsNullOrEmpty(values[22])&&
                   !String.IsNullOrEmpty(values[23]))
                {
                    Cultivation cultivation = new Cultivation();
                    cultivation.CultivationDateStart = Convert.ToInt32(values[20]);
                    cultivation.CultivationDateEnd = Convert.ToInt32(values[21]);
                    cultivation.GerminationTemperature = Convert.ToDouble(values[22]);
                    cultivation.GerminationPeriodDays = Convert.ToInt32(values[23]);

                    plant.Cultivation = cultivation;
                }

                plant.Type = TaxonType.SubSpecies;

                #endregion


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return plant;

        }

    }
}
