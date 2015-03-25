using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MCT.Helpers
{
    public class AppConfigHelper
    {
        public static string GetRoot()
        {
            return HttpContext.Current.Server.MapPath("~");
        }

        public static string GetWorkspace()
        {
            return "..\\..\\Workspace";
        }

    }
}
