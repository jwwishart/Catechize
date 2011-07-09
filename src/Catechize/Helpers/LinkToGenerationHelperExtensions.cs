using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Catechize.Resources;
using Catechize.Controllers;
using System.Web.Routing;
using Catechize.Model;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Catechize.Helpers
{
    public static class LinkToGenerationHelperExtensionMethods
    {
        private const string CoursesControllerName = "Courses";

        // Testing Methods 
        //

        public static MvcHtmlString Action<T>(this LinkToGenerationHelper helper, 
            Expression<Func<T, ActionResult>> controllerAction) where T : Controller
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            
            //var method = ((MethodCallExpression)controllerAction.Body).Method;

            //var arguments = ((MethodCallExpression)controllerAction.Body).Arguments;
            
            //var parameters = method.GetParameters();
            var values = new RouteValueDictionary();

            //foreach (ParameterInfo pi in parameters)
            //{
            //    values.Add(pi.Name, ((System.Linq.Expressions.)arguments[0]).Method.Invoke(null, new object][] { }));
            //}

            var url = urlHelper.Action(((MethodCallExpression)controllerAction.Body).Method.Name
                , typeof(T).Name.Replace("Controller", ""), values);

            return ConstructAnchor(helper, url, LinkResources.Courses);
        }



        // Extension Methods
        //

        public static MvcHtmlString ListCourses(this LinkToGenerationHelper helper)
        {
            return Construct(helper, LinkResources.Courses, "Index", CoursesControllerName);
        }

        public static MvcHtmlString NewCourse(this LinkToGenerationHelper helper)
        {
            return Construct(helper, LinkResources.NewCourse, "NewCourse", CoursesControllerName);
        }

        public static MvcHtmlString EditCourse(this LinkToGenerationHelper helper, Course course)
        {
            // Determine localized name
            string linkText = LinkResources.ViewCourse;
            CourseLanguage lang = course.GetLanguage(CultureInfo.CurrentUICulture);

            if (lang != null)
                linkText = lang.Title;
            else
                linkText = course.Title;

            return EditCourse(helper, course, linkText);
        }

        public static MvcHtmlString EditCourse(this LinkToGenerationHelper helper, Course course, string label)
        {
            return Construct(helper, label, "EditCourse", CoursesControllerName, new { courseName = course.CourseID });
        }

        public static MvcHtmlString ViewCourse(this LinkToGenerationHelper helper, Course course)
        {
            // Determine localized name
            string linkText = LinkResources.ViewCourse;
            CourseLanguage lang = course.GetLanguage(CultureInfo.CurrentUICulture);

            if (lang != null)
                linkText = lang.Title;
            else
                linkText = course.Title;

            return ViewCourse(helper, course, linkText);
        }

        public static MvcHtmlString ViewCourse(this LinkToGenerationHelper helper, Course course, string label)
        {
            return Construct(helper, label, "ViewCourse", CoursesControllerName, new { courseName = course.CourseID });
        }

        public static MvcHtmlString RegisterForCourse(this LinkToGenerationHelper helper, Course course) {
            return RegisterForCourse(helper, course, LinkResources.Register);
        }

        public static MvcHtmlString RegisterForCourse(this LinkToGenerationHelper helper, Course course, string label)
        {
            return Construct(helper, label, "Register", CoursesControllerName, new { courseName = course.CourseID });
        }

        public static MvcHtmlString ViewMyAccount(this LinkToGenerationHelper helper)
        {
            return Construct(helper, "My Account", "Index", "User", new { username = helper.ViewContext.RequestContext.HttpContext.User.Identity.Name });
        }

        public static MvcHtmlString ChangePassword(this LinkToGenerationHelper helper) {
            return Construct(helper, "Change Password", "ChangePassword", "Account");
        }


        // Helpers Methods
        //

        private static MvcHtmlString Construct(LinkToGenerationHelper helper, string label, string action, string controller)
        {
            return Construct(helper, label, action, controller, new RouteValueDictionary());
        }

        private static MvcHtmlString Construct(LinkToGenerationHelper helper, string label, string action, string controller, object routeValues)
        {
            return Construct(helper, label, action, controller, new RouteValueDictionary(routeValues));
        }


        private static MvcHtmlString Construct(LinkToGenerationHelper helper, string label, string action, string controller, RouteValueDictionary routeValues)
        {
            return Construct(helper, label, action, controller, routeValues, string.Empty, string.Empty);
        }

        private static MvcHtmlString Construct(LinkToGenerationHelper helper, string label, string action, string controller, object routeValues, string title, string cssClass)
        {
            return Construct(helper, label, action, controller, new RouteValueDictionary(routeValues), string.Empty, string.Empty);
        }

        private static MvcHtmlString Construct(LinkToGenerationHelper helper, string label, string action, string controller, RouteValueDictionary routeValues, string title, string cssClass)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);

            return ConstructAnchor(helper, url, label, title, cssClass);
        }

        private static MvcHtmlString ConstructAnchor(LinkToGenerationHelper helper, string url, string text, string title = "", string cssClass = "")
        {
            TagBuilder builder = new TagBuilder("a");
            builder.Attributes.Add("href", url);
            builder.InnerHtml = text;

            if (false == String.IsNullOrEmpty(title))
                builder.Attributes.Add("title", title);

            if (false == String.IsNullOrEmpty(cssClass))
                builder.Attributes.Add("class", cssClass);

            return MvcHtmlString.Create(builder.ToString());
        }

    }

    
}