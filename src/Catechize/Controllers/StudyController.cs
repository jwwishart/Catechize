using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catechize.Controllers
{
    public class StudyController : Controller
    {
        //
        // GET: /Course/

        public ActionResult HandlePageRequest(string courseName, int coursePart, string coursePage)
        {
            ViewBag.courseName = courseName;
            ViewBag.coursePart = coursePart;
            ViewBag.coursePage = coursePage;
            return View();
        }

    }
}
