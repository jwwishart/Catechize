using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catechize.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(string TODO_Stub)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Security()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Security(string TODO_Stub)
        {
            return View();
        }

        public ActionResult LinkAccount()
        {
            // TODO: This is for linking your account to twitter or another authentication mechanism.
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}
