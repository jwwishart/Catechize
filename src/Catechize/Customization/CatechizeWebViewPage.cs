using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catechize.Helpers;

namespace Catechize.Customization
{
    public abstract class CatechizeWebViewPage : WebViewPage
    {
        public UrlGenerationHelper UrlGen { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            UrlGen = new UrlGenerationHelper(base.ViewContext, this);
        }
    }

    public abstract class CatechizeWebViewPage<T> : CatechizeWebViewPage
    {
        public UrlGenerationHelper<T> UrlGen { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            UrlGen = new UrlGenerationHelper<T>(base.ViewContext, this);
        }
    }
}