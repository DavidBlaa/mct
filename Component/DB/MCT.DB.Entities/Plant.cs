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
        public virtual LocationType LocationType { get; set; }
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
            LocationType = LocationType.Unknown;
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

            this.AfterCultures = newPlant.AfterCultures;
            this.Bloom = newPlant.Bloom;
            this.Cultivation = newPlant.Cultivation;
            this.Description = newPlant.Description;
            this.Harvest = newPlant.Harvest;
            this.Height = newPlant.Height;
            this.Id = newPlant.Id;
            this.LocationType = newPlant.LocationType;
            this.Medias = newPlant.Medias;
            this.Name = newPlant.Name;
            this.NutrientClaim = newPlant.NutrientClaim;
            this.Parent = newPlant.Parent;
            this.PreCultures = newPlant.PreCultures;
            this.Rank = newPlant.Rank;
            this.RootDepth = newPlant.RootDepth;
            this.ScientificName = newPlant.ScientificName;
            this.SeedMaturity = newPlant.SeedMaturity;
            this.Sowing = newPlant.Sowing;
            this.SowingDepth = newPlant.SowingDepth;
            this.Width = newPlant.Width;
        }


    }

    public enum LocationType
    {
        Unknown,
        Sunny,
        Shady,
        PartialShade
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
