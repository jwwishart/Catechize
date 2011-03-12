using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Catechize.Resources;
using Catechize.Controllers;
using System.Web.Routing;

namespace Catechize.Helpers
{
    public static class LinkToGenerationHelperExtensionMethods
    {
        private const string CoursesRouteName = "Courses";
        private const string CoursesControllerName = "Courses";

        private static string GenerateLink(LinkToGenerationHelper helper, string linkText, string routeName, string action, string controller)
        {
            return HtmlHelper.GenerateLink(
                  helper.ViewContext.RequestContext
                , helper.RouteCollection
                , linkText
                , routeName
                , action
                , controller
                , new RouteValueDictionary()
                , new RouteValueDictionary());
        }

        public static string CoursesPath(this LinkToGenerationHelper helper)
        {
            return GenerateLink(helper, LinkResources.Courses, CoursesRouteName, string.Empty, CoursesControllerName);
        }

        public static string NewCoursePath(this LinkToGenerationHelper helper, string courseKey)
        {
            return string.Empty;
        }

        public static string EditCoursePath(this LinkToGenerationHelper helper, string courseKey)
        {
            return string.Empty;
        }

        public static string CoursePath(this LinkToGenerationHelper helper, string courseKey)
        {
            return string.Empty;
        }
    }

    
}