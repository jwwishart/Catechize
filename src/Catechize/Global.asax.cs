using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Catechize
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore Routes
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Map Study page Route
            routes.MapRoute(
                "Study",
                "Study/{courseName}/Part{coursePart}/{coursePage}",
                new { controller="Study", action="HandlePageRequest", 
                      courseName = "Basic", coursePart = 1, coursePage = "index" }
            );

            routes.MapRoute(
                "User",
                "User/{username}/{section}",
                new { controller="User", action="index",
                      username= UrlParameter.Optional, section = UrlParameter.Optional }
            );

            // Map Courses details Route
            routes.MapRoute(
                "Courses",
                "Courses/{courseName}/{action}",
                new { controller="Courses", action="Index",
                      courseName = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            // Admin Controller
            routes.MapRoute(
                "Admin",
                "Admin/{adminArea}",
                new { controller = "Admin", action = "Index",
                    adminArea = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}