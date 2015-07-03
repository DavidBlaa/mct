using System.IO;
using System.Web;

namespace MCT.Helpers
{
    public class AppConfigHelper
    {
        public static string GetRoot()
        {
            return HttpContext.Current.Server.MapPath("~");
        }

        public static string GetWorkspaceForClient()
        {
            return "..\\..\\Workspace";
        }

        public static string GetWorkspace()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~"),"Workspace");
        }

    }
}
