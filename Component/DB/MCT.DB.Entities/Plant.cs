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
        public virtual RootDepth RootDepth { get; set; }
        public virtual NutrientClaim NutrientClaim { get; set; }
        public virtual int SowingDepth { get; set; }
     
        #endregion

        #region Associations

        //Vorzucht
        public virtual Cultivation Cultivation { get; set; }

        //Dates
        public virtual ICollection<TimePeriod> Sowing { get; set; }
        public virtual ICollection<TimePeriod> Bloom { get; set; }
        public virtual ICollection<TimePeriod> Harvest { get; set; }
        public virtual ICollection<TimePeriod> SeedMaturity { get; set; }

        #endregion

        public Plant()
        {
            Sowing = new List<TimePeriod>();
            Bloom = new List<TimePeriod>();
            Harvest = new List<TimePeriod>();
            SeedMaturity = new List<TimePeriod>();
        }


    }

    public enum RootDepth
    { 
        Flat,
        Medium,
        Deep
    }

    public enum NutrientClaim
    {
        Strong,
        Medium,
        Weak
    }

    public class PlantHelper
    {

        public static RootDepth GetRootDepth(string value)
        {
            RootDepth result;
            if (Enum.TryParse<RootDepth>(value, out result))
            {
                return result;
            }

            return RootDepth.Flat;
        }

        public static NutrientClaim GetNutrientClaimDepth(string value)
        {
            NutrientClaim result;
            if (Enum.TryParse<NutrientClaim>(value, out result))
            {
                return result;
            }

            return NutrientClaim.Weak;
        }
    }
}
