using MCT.DB.Entities;
using MCT.Helpers;

namespace MCT.DB.Services
{
    public class InteractionManager : ManagerBase<Interaction, long>
    {
        public InteractionManager()
        {
            CurrentNHibernateSession = NHibernateHelper.GetCurrentSession();
        }

    }
}
