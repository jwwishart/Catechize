using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using Catechize.Services;

namespace Catechize.Controllers
{
    using Catechize.Helpers;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //// return this.Redirect<HomeController>( c => c.About() );
            //return this.Redirect<AccountController>(c => c.ChangePassword());
            ////return this.Redirect<AccountController>(c => c.Register());

            ViewBag.Message = "Welcome";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
