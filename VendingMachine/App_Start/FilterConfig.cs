using System.Web;
using System.Web.Mvc;

namespace Test_Fajar_INSPIRO
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
