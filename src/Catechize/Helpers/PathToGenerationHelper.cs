using System.Web.Mvc;
using System.Web.Routing;

public class PathToGenerationHelper
{
    public PathToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        : this(viewContext, viewDataContainer, RouteTable.Routes)
    {
    }

    public PathToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer
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

public class PathToGenerationHelper<T>
{
    public PathToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        : this(viewContext, viewDataContainer, RouteTable.Routes)
    {
    }

    public PathToGenerationHelper(ViewContext viewContext, IViewDataContainer viewDataContainer
        , RouteCollection routeCollection)
    {
        this.ViewContext = viewContext;
        this.ViewData = new ViewDataDictionary<T>(viewDataContainer.ViewData);
        this.RouteCollection = routeCollection;
    }

    public ViewDataDictionary<T> ViewData
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