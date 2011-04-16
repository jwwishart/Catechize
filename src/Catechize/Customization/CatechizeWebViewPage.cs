using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catechize.Helpers;

namespace Catechize.Customization
{

    // Kudos: http://haacked.com/archive/2011/02/21/changing-base-type-of-a-razor-view.aspx
    // But it didn't quite work correctly.
    public abstract class CatechizeWebViewPage<TModel> : WebViewPage<TModel>
    {
        public LinkToGenerationHelper<TModel> LinkTo { get; set; }

        public override void InitHelpers()
        {
            base.InitHelpers();

            this.LinkTo = new LinkToGenerationHelper<TModel>(base.ViewContext, this);
        }
    }
}