using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catechize.Controllers
{
    public class CoursesController : Controller
    {
        public ActionResult Index(string courseName)
        {
            return View();
        }

    } // End Controller
}
