using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Catechize.Helpers
{
    public static class ControllerHelpers
    {
        public static ActionResult RedirectToUnauthorized(this Controller controller) {
            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("controller", "Account");
            values.Add("action", "Unauthorized");

            return new RedirectToRouteResult(values);
        }
    }
}