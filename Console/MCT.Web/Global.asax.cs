using NHibernate;
using NHibernate.Cfg;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebService;

namespace MCT.Web
{
    public class MvcApplication : HttpApplication
    {
        public static ISessionFactory NHibernateSessionFactory;

        //public override void Init()
        //{
        //    //this.BeginRequest += (sender, e) =>
        //    //{

        //    //    var session = NHibernateSessionFactory.OpenSession();
        //    //    CurrentSessionContext.Bind(session);
        //    //};

        //    //this.EndRequest += (sender, e) =>
        //    //{

        //    //    var session = CurrentSessionContext.Unbind(NHibernateSessionFactory);
        //    //    session.Dispose();
        //    //};

        //    //if (NHibernateSessionFactory != null) NHibernateSessionFactory = new Configuration().Configure().BuildSessionFactory();

        //    base.Init();
        //}

        protected void Application_Start()
        {
            NHibernateSessionFactory = new Configuration().Configure().BuildSessionFactory();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
