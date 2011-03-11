using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catechize.Helpers;

namespace Catechize.Customization
{
    public class CatechizeWebViewPage : WebViewPage
    {
        public UrlGenerationHelper UrlGen { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            UrlGen = new UrlGenerationHelper(base.ViewContext, this);
        }
    }
}