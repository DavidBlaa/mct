using System;
using System.Collections.Generic;
using NHibernate.Search.Attributes;

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
        public virtual ICollection<Sowing> Sowing { get; set; }
        public virtual ICollection<Bloom> Bloom { get; set; }
        public virtual ICollection<Harvest> Harvest { get; set; }
        public virtual ICollection<SeedMaturity> SeedMaturity { get; set; }

        #endregion

        public Plant()
        {
            Sowing = new List<Sowing>();
            Bloom = new List<Bloom>();
            Harvest = new List<Harvest>();
            SeedMaturity = new List<SeedMaturity>();
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
            if (Enum.TryParse(value, out result))
            {
                return result;
            }

            return RootDepth.Flat;
        }

        public static NutrientClaim GetNutrientClaimDepth(string value)
        {
            NutrientClaim result;
            if (Enum.TryParse(value, out result))
            {
                return result;
            }

            return NutrientClaim.Weak;
        }
    }
}
