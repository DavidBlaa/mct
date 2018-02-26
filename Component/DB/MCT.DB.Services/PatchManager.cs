using MCT.DB.Entities.PatchPlaner;
using MCT.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Services
{
    public class PatchManager : ManagerBase<Patch, long>
    {
        public PatchManager()
        {
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

    }
}
