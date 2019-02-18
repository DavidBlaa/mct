using MCT.DB.Entities;
using MCT.Helpers;
using System.Collections.Generic;

namespace MCT.DB.Services
{
    public class InteractionManager : ManagerBase<Interaction, long>
    {
        public InteractionManager()
        {
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

        public IEnumerable<Interaction> GetAll()
        {
            return GetAll<Interaction>();
        }
    }
}