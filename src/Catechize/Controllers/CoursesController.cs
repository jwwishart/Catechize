using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catechize.Services;

namespace Catechize.Controllers
{
    public class CoursesController : Controller
    {
        private ICatechizeControllerService services = null;

        public CoursesController(ICatechizeControllerService services)
        {
            if (services == null) throw new NullReferenceException("services cannot be null");

            this.services = services;
        }

        public ActionResult Index(string courseName)
        {
            return View();
        }

    } // End Controller
}
