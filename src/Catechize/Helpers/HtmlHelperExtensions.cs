using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Security.Principal;
using Catechize.Templating;

namespace Catechize.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Script(this HtmlHelper helper, string scriptFilename)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("script");

            if (Path.GetExtension(scriptFilename) == ".js") {
                builder.Attributes.Add("src", urlHelper.Content("~/Scripts/" + scriptFilename));
            } else {
                builder.Attributes.Add("src", urlHelper.Content("~/Scripts/" + scriptFilename + ".js"));
            }

            return MvcHtmlString.Create( builder.ToString() );
        }

        public static MvcHtmlString Stylesheet(this HtmlHelper helper, string cssFilename)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("link");

            if (Path.GetExtension(cssFilename) == ".css")
                builder.Attributes.Add("href", urlHelper.Content("~/Content/" + cssFilename));
            else
                builder.Attributes.Add("href", urlHelper.Content("~/Content/" + cssFilename + ".css"));

            builder.Attributes.Add("rel", "stylesheet");
            builder.Attributes.Add("type", "text/css");
            
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString RenderTemplate( this HtmlHelper helper, string templateName ) {
            return HtmlHelperExtensions.RenderTemplate( helper, templateName, null );
        }

        public static MvcHtmlString RenderTemplate( this HtmlHelper helper, string templateName, object model ) {
            var content = TemplatingFactory.GetTemplateLoader().Load( templateName );

            return MvcHtmlString.Create(
                TemplatingFactory.GetTemplateEngine().ProcessTemplate( content, model ) );
        }
    }
}