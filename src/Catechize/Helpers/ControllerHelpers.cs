using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Reflection;

namespace Catechize.Helpers
{
    public static class ControllerHelpers
    {
        public static ActionResult Redirect<T>(this Controller controller, Expression<Func<T, ActionResult>> controllerAction) where T : Controller
        {
            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("controller", typeof(T).Name.Replace("Controller", ""));
            values.Add("action", ((MethodCallExpression)controllerAction.Body).Method.Name);

            var method = ((MethodCallExpression)controllerAction.Body).Method;

            var parameters = method.GetParameters();
            
            foreach (ParameterInfo pi in parameters)
            {
                values.Add(pi.Name, "value");
            }

            return new RedirectToRouteResult(values);
        }

        public static ActionResult RedirectToHome(this Controller controller)
        {
            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("controller", "Home");
            values.Add("action", "Index");

            return new RedirectToRouteResult(values);
        }
        
        public static ActionResult RedirectToUnauthorized(this Controller controller) {
            RouteValueDictionary values = new RouteValueDictionary();
            values.Add("controller", "Account");
            values.Add("action", "Unauthorized");

            return new RedirectToRouteResult(values);
        }
    }
}