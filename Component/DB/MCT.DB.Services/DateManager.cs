using MCT.DB.Entities;
using MCT.Helpers;

namespace MCT.DB.Services
{
    public class DateManager:ManagerBase<Day, long>
    {

        public DateManager()
        {

            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }
    }
}
