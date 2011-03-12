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
        public UrlToGenerationHelper UrlTo { get; set; }
        public PathToGenerationHelper PathTo { get; set; }
        public LinkToGenerationHelper LinkTo { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            this.UrlTo = new UrlToGenerationHelper(base.ViewContext, this);
            this.PathTo = new PathToGenerationHelper(base.ViewContext, this);
            this.LinkTo = new LinkToGenerationHelper(base.ViewContext, this);
        }
    }

    public abstract class CatechizeWebViewPage<T> : CatechizeWebViewPage
    {
        public new UrlToGenerationHelper<T> UrlTo { get; set; }
        public new PathToGenerationHelper<T> PathTo { get; set; }
        public new LinkToGenerationHelper<T> LinkTo { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            this.UrlTo = new UrlToGenerationHelper<T>(base.ViewContext, this);
            this.PathTo = new PathToGenerationHelper<T>(base.ViewContext, this);
            this.LinkTo = new LinkToGenerationHelper<T>(base.ViewContext, this);
        }
    }
}