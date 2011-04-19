using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Catechize.Services;
using Catechize.Customization;
using System.Web.Security;
using Elmah;
using System.Security.Principal;
using Ninject;

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

            // Map Routes
            routes.MapRoute(
                "Study",
                "Study/{courseKey}/Part{coursePart}/{coursePage}",
                new { controller="Study", action="HandlePageRequest", 
                      courseKey = "Basic", coursePart = 1, coursePage = "index" }
            );

            routes.MapRoute(
                "User",
                "User/{username}/{action}",
                new { controller="User", action="index" }
            );

            routes.MapRoute(
                "Courses",
                "Courses/{action}/{courseName}",
                new { controller="Courses", action="Index" }
            );

            routes.MapRoute(
                "Admin",
                "Admin/{adminArea}",
                new { controller = "Admin", action = "Index",
                    adminArea = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Design",
                "Design/{courseKey}/{page}",
                new { controller="Design", action="Index",
                      courseKey = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Translate",
                "Translate/{courseKey}/{page}",
                new { controller="Translate", action="Index",
                      courseKey = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Grading",
                "Grading/{courseKey}/{page}",
                new { controller="Grading", action="Index",
                      courseKey = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null || authCookie.Value == "")
                return;

            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(
                    new ArgumentException("authentication cookie could not be decripted", ex));
                return;
            }

            if (Context.User != null)
            {
                var membershipService = DependencyResolver.Current
                    .GetService<IMembershipService>();

                var principal = new GenericPrincipal( 
                    new GenericIdentity(authTicket.Name, "CatechizeCustom"),
                    authTicket.UserData.Split(',') /* User Roles */);

                Context.User = principal;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // TODO: Remove before publishing
            Database.SetInitializer<CatDbContext>(new CourseInitializer());
        }
    }
}