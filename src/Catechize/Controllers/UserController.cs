using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catechize.Models;
using System.Web.Security;

namespace Catechize.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index(string username)
        {
            PrivateUserProfileModel model;

            if (User.Identity != null)
            {
                if (username.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                {
                    model = new PersonalUserProfileModel();
                    model.Username = username;
                    model.Email = Membership.GetUser(username).Email;
                    return View("Account_Personal", model);
                }
                else
                {
                    // View only
                    model = new PrivateUserProfileModel();
                    model.Username = username;
                    model.Email = Membership.GetUser(username).Email;
                    return View("Account_Public", model);
                }
            }

            // Anonymous Users
            return View("Account_Private");
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
