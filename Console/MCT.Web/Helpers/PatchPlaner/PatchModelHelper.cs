using MCT.DB.Entities.PatchPlaner;
using MCT.Web.Models;
using MCT.Web.Models.PatchPlaner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Helpers.PatchPlaner
{
    public class PatchModelHelper
    {

        /// <summary>
        /// Convert a patch to a patchModel
        /// </summary>
        /// <param name="patch"></param>
        /// <returns>PatchModel</returns>
        public static PatchModel ConvertTo(Patch patch)
        {
            PatchModel model = new PatchModel();

            model.Id = patch.Id;
            model.Name = patch.Name;
            model.Description = patch.Description;
            model.Height = patch.Height;
            model.Width = patch.Width;
            model.LocationType = patch.LocationType;
            model.NutrientClaim = patch.NutrientClaim;

            //placmenets
            foreach (var p in patch.Placements)
            {
                model.Placements.Add(ConvertTo(p));
            }

            return model;

        }

        //ToDo create Patch from PatchModel

        /// <summary>
        /// Convert a patch model to a patch widthout placemenents
        /// </summary>
        /// <param name="patchModel"></param>
        /// <returns>Patch</returns>
        public static Patch ConvertTo(PatchModel patchModel)
        {
            Patch patch = new Patch();

            patch.Id = patchModel.Id;
            patch.Name = patchModel.Name;
            patch.Description = patchModel.Description;
            patch.Height = patchModel.Height;
            patch.Width = patchModel.Width;
            patch.LocationType = patchModel.LocationType;
            patch.NutrientClaim = patchModel.NutrientClaim;

            ////placmenets
            //foreach (var p in patch.Placements)
            //{
            //    model.Placements.Add(ConvertTo(p));
            //}

            return patch;

        }

        //ToDo create Placement from PlacementModel

        //ToDo create placementModel from Placement
        /// <summary>
        /// Convert a Placement in to a PlacmentModel
        /// </summary>
        /// <param name="placement"></param>
        /// <returns>Placement Model</returns>
        public static PlacementModel ConvertTo(Placement placement)
        {
            if (placement.Id <= 0) return null;
            if (String.IsNullOrEmpty(placement.Transformation)) return null;


            PlacementModel model = new PlacementModel();
            model.Id = placement.Id;
            model.PlantingArea = placement.PlantingArea;
            model.PlantingMonth = placement.PlantingMonth;
            model.Transformation = placement.Transformation;

            if (placement.Patch != null)
                model.PatchId = placement.Patch.Id;

            if (placement.Plant != null)
                model.Plant = PlantModel.Convert(placement.Plant);

            return model;
        }

        

    }
}