using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catechize.Customization
{
    // Kudos: http://helios.ca/2009/05/27/aspnet-mvc-action-filter-ajax-only-attribute/#comment-490
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest() == false)
            {
                filterContext.Result = new HttpNotFoundResult();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
