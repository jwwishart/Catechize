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
    public static class PathToGenerationHelperExtensionMethods
    {
        private const string CoursesRouteName = "Courses";
        private const string CoursesControllerName = "Courses";

        public static string CoursesPath(this PathToGenerationHelper helper)
        {
            return string.Empty;
        }

        public static string NewCoursePath(this PathToGenerationHelper helper, string courseKey)
        {
            return string.Empty;
        }

        public static string EditCoursePath(this PathToGenerationHelper helper, string courseKey)
        {
            return string.Empty;
        }

        public static string CoursePath(this PathToGenerationHelper helper, string courseKey)
        {
            return string.Empty;
        }
    }

    
}