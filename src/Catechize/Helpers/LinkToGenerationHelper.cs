using System.Web.Mvc;
using System.Web.Routing;

namespace Catechize.Helpers
{
    public class LinkToGenerationHelper
    {
        public LinkToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
        }

        public LinkToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer
            , RouteCollection routeCollection)
        {
            this.ViewContext = viewContext;
            this.ViewData = new ViewDataDictionary(viewDataContainer.ViewData);
            this.RouteCollection = routeCollection;
        }

        public ViewDataDictionary ViewData
        {
            get;
            private set;
        }

        public ViewContext ViewContext
        {
            get;
            private set;
        }

        public RouteCollection RouteCollection
        {
            get;
            private set;
        }
    }

    public class LinkToGenerationHelper<T> : LinkToGenerationHelper
    {
        public LinkToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
        }

        public LinkToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer
            , RouteCollection routeCollection)
            : base(viewContext, viewDataContainer, routeCollection)
        {
            this.ViewContext = viewContext;
            this.ViewData = new ViewDataDictionary<T>(viewDataContainer.ViewData);
            this.RouteCollection = routeCollection;
        }

        public new ViewDataDictionary<T> ViewData
        {
            get;
            private set;
        }

        public new ViewContext ViewContext
        {
            get;
            private set;
        }

        public new RouteCollection RouteCollection
        {
            get;
            private set;
        }
    }
}