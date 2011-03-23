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
        public LinkToGenerationHelper LinkTo { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();

            this.LinkTo = new LinkToGenerationHelper<object>(base.ViewContext, this);
        }
    }

    public abstract class CatechizeWebViewPage<T> : CatechizeWebViewPage
    {
    }
}