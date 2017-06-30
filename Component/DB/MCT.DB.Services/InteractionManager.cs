using System.Collections.Generic;
using System.Linq;
using MCT.DB.Entities;
using MCT.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

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
