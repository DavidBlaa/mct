using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Entities
{
    public class Plant:Subject
    {

        #region Attributes

        //Name und Hierarchie
        public virtual string ScientificName { get; set; }
        public virtual string ClassName { get; set; }
        public virtual string OrderName { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string GenusName { get; set; }
  
        //Eigeschaften 
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }
        public virtual string RootDetph { get; set; }
        public virtual string NutrientClaim { get; set; }
        public virtual string SowingDepth { get; set; }
     
        //Vorzucht
        public virtual long Cultivation { get; set; }

        //Dates
        public virtual DateTime SowingStart { get; set; }
        public virtual DateTime SowingEnd { get; set; }
        public virtual DateTime BloomStart { get; set; }
        public virtual DateTime BloomEnd { get; set; }
        public virtual DateTime HarvestStart { get; set; }
        public virtual DateTime HarvestEnd { get; set; }
        public virtual DateTime SeedMaturityStart { get; set; }
        public virtual DateTime SeedMaturityEnd { get; set; }

        #endregion

        #region Associations


        #endregion

    }
}
