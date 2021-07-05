using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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

        #endregion Attributes

        #region Associations

        public virtual ICollection<Plant> PreCultures { get; set; }
        public virtual ICollection<Plant> AfterCultures { get; set; }

        #endregion Associations

        public Plant()
        {
            TimePeriods = new List<TimePeriod>();
            PreCultures = new List<Plant>();
            AfterCultures = new List<Plant>();
            RootDepth = RootDepth.Empty;
            NutrientClaim = NutrientClaim.Empty;
            LocationType = LocationType.Unknown;
            Width = 0;
            Height = 0;
        }

        public virtual Plant Self { get { return this; } }

        /// <summary>
        /// XXX merge statt replace
        /// </summary>
        /// <param name="newPlant"></param>
        public virtual void Update(Plant newPlant)
        {
            // update subject
            this.TimePeriods = newPlant.TimePeriods;
            this.AfterCultures = newPlant.AfterCultures;
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
            this.SowingDepth = newPlant.SowingDepth;
            this.Width = newPlant.Width;
        }
    }

    public enum LocationType
    {
        [Display(Name = "Unbekannt")]
        [EnumMember(Value = "Unbekannt")]
        Unknown,

        [Display(Name = "Sonnig")]
        [EnumMember(Value = "Sonnig")]
        Sunny,

        [Display(Name = "Schattig")]
        [EnumMember(Value = "Schattig")]
        Shady,

        [Display(Name = "Halbschattig")]
        [EnumMember(Value = "Halbschattig")]
        PartialShade
    }

    public enum RootDepth
    {
        [Display(Name = "Nicht bekannt")]
        [EnumMember(Value = "Nicht bekannt")]
        Empty,

        [Display(Name = "Flach")]
        [EnumMember(Value = "Flach")]
        Flat,

        [Display(Name = "Mittel")]
        [EnumMember(Value = "Mittel")]
        Medium,

        [Display(Name = "Tief")]
        [EnumMember(Value = "Tief")]
        Deep
    }

    public enum NutrientClaim
    {
        [Display(Name = "Unbekannt")]
        [EnumMember(Value = "Unbekannt")]
        Empty,

        [Display(Name = "Stark")]
        [EnumMember(Value = "Stark")]
        Strong,

        [Display(Name = "Mittel")]
        [EnumMember(Value = "Mittel")]
        Medium,

        [Display(Name = "Schwach")]
        [EnumMember(Value = "Schwach")]
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