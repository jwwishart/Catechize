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
        private const string CoursesControllerName = "Courses";

        // Helpers Methods
        //
        private static MvcHtmlString GenerateAnchor(LinkToGenerationHelper helper, string url, string text) {
            TagBuilder builder = new TagBuilder("a");
            builder.Attributes.Add("href", url);
            builder.InnerHtml = text;
            return MvcHtmlString.Create(builder.ToString());
        }


        // Helpers (Extension Methods)
        //
        public static MvcHtmlString CoursesLink(this LinkToGenerationHelper helper)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("Index", CoursesControllerName);

            return GenerateAnchor(helper, url, LinkResources.Courses);
        }

        public static MvcHtmlString NewCourseLink(this LinkToGenerationHelper helper)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("New", CoursesControllerName);

            return GenerateAnchor(helper, url, LinkResources.NewCourse);
        }

        public static MvcHtmlString EditCourseLink(this LinkToGenerationHelper helper, string courseKey)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("Edit", CoursesControllerName, new { courseKey = courseKey });

            return GenerateAnchor(helper, url, LinkResources.EditCourse);
        }

        public static MvcHtmlString CourseLink(this LinkToGenerationHelper helper, string courseKey)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("View", CoursesControllerName, new { courseKey = courseKey });

            return GenerateAnchor(helper, url, LinkResources.ViewCourse);
        }
    }

    
}