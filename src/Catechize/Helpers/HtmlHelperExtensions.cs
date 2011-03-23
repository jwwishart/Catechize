using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Catechize.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string IncludeScript(this HtmlHelper helper, string scriptFilename)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("script");

            if (Path.GetExtension(scriptFilename) == ".js") {
                builder.Attributes.Add("src", urlHelper.Content("~/Scripts/" + scriptFilename));
            } else {
                builder.Attributes.Add("src", urlHelper.Content("~/Scripts/" + scriptFilename + ".js"));
            }

            return builder.ToString() ;
        }
    }
}