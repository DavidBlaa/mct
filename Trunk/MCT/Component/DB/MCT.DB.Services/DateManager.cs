using MCT.DB.Entities;
using MCT.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.DB.Services
{
    public class DateManager:ManagerBase<Day, long>
    {

        public DateManager()
        {

            base.CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }
    }
}
