using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Plant:Species
    {

        #region Attributes
        //Eigeschaften 
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }
        public virtual string RootDetph { get; set; }
        public virtual string NutrientClaim { get; set; }
        public virtual string SowingDepth { get; set; }
     
        //Vorzucht
        public virtual Cultivation Cultivation { get; set; }

        //Dates
        public virtual int SowingStart { get; set; }
        public virtual int SowingEnd { get; set; }
        public virtual int BloomStart { get; set; }
        public virtual int BloomEnd { get; set; }
        public virtual int HarvestStart { get; set; }
        public virtual int HarvestEnd { get; set; }
        public virtual int SeedMaturityStart { get; set; }
        public virtual int SeedMaturityEnd { get; set; }

        #endregion

        #region Associations


        #endregion

    }
}
