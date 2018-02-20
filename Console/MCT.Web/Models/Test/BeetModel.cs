using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCT.Web.Models.Test
{
    public class BeetModel
    {
        public List<PflanzenModel> Pflanzen;

        public BeetModel()
        {
            Pflanzen = new List<PflanzenModel>();
        }
    }
}