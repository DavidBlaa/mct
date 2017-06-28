using System;
using System.Collections.Generic;

namespace MCT.DB.Entities
{

    public class Plant : Species
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

        public virtual ICollection<Plant> PreCultures { get; set; }
        public virtual ICollection<Plant> AfterCultures { get; set; }

        #endregion

        public Plant()
        {
            Sowing = new List<Sowing>();
            Bloom = new List<Bloom>();
            Harvest = new List<Harvest>();
            SeedMaturity = new List<SeedMaturity>();
            PreCultures = new List<Plant>();
            AfterCultures = new List<Plant>();
            RootDepth = RootDepth.Empty;
            NutrientClaim = NutrientClaim.Empty;
            Width = 0;
            Height = 0;
        }


        /// <summary>
        /// XXX merge statt replace
        /// </summary>
        /// <param name="newPlant"></param>
        public virtual void Update(Plant newPlant)
        {
            // update subject

            Description += " " + newPlant.Description;

            if (Parent == null)
                Parent = newPlant.Parent;

            //update species
            if (Sowing.Count == 0)
                Sowing = newPlant.Sowing;

            if (Bloom.Count == 0)
                Bloom = newPlant.Bloom;

            if (Harvest.Count == 0)
                Harvest = newPlant.Harvest;

            if (PreCultures.Count == 0)
                //PreCultures = newPlant.PreCultures;

                if (AfterCultures.Count == 0)
                    //AfterCultures = newPlant.AfterCultures;

                    if (RootDepth.Equals(RootDepth.Empty))
                        RootDepth = newPlant.RootDepth;

            if (NutrientClaim.Equals(NutrientClaim.Empty))
                NutrientClaim = newPlant.NutrientClaim;

            if (Width == 0)
                Width = newPlant.Width;

            if (Height == 0)
                Height = newPlant.Height;

        }


    }

    public enum RootDepth
    {
        Empty,
        Flat,
        Medium,
        Deep
    }

    public enum NutrientClaim
    {
        Empty,
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
