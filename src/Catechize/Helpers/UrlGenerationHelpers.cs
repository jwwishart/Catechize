using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Catechize.Resources;

namespace Catechize.Helpers
{
    public static class UrlGenerationHelpers
    {
        // Goto the page listing all courses
        public static MvcHtmlString CoursesUrl(this HtmlHelper helper)
        {
            return helper.RouteLink(LinkResources.Courses, "Courses");
        }

        // Goto a specific course 
        public static MvcHtmlString CourseViewUrl(this HtmlHelper helper, string courseKey)
        {
            return helper.RouteLink(LinkResources.ViewCourse, "Courses", new { courseName = courseKey });
        }
    }
}