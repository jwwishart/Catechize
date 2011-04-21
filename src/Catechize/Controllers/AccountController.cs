using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Catechize.Models;
using System.Text;
using Catechize.Services;
using Catechize.Customization;
using Microsoft.Web.Helpers;
using System.Configuration;

namespace Catechize.Controllers
{
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }


        // Constructors
        //

        public AccountController(
            IFormsAuthenticationService formsService,
            IMembershipService membershipService)
        {
            this.FormsService = formsService;
            this.MembershipService = membershipService;
        }

        // TODO: remove for production
        public ActionResult SetupDatabaseDefaults()
        {
            // Note that you might have to adjust the web.config file
            // membership settings to get these appropriately created.
            Membership.CreateUser("master", "qwerty", "jwwishart@hotmail.com");
            Membership.CreateUser("jwwishart", "qwerty", "jwwishart@gmail.com");

            Roles.CreateRole("Master");
            Roles.CreateRole("Administrator");
            Roles.CreateRole("Translator");
            Roles.CreateRole("Student");
            Roles.CreateRole("Designer");
            Roles.CreateRole("Grader");
            Roles.CreateRole("Manager");

            Roles.AddUsersToRole(new string[] { "master" }, "Master");
            Roles.AddUsersToRole(new string[] { "jwwishart" }, "student");

            return RedirectToAction("Index", "Home");
        }


        // **************************************
        // URL: /Account/LogOn
        // **************************************


        public ActionResult LogOn()
        {
            // Redirect the Use if they are authenticated and are sent here
            // TODO: This might not be 100% adequate?
            // Kudos: http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-aut/705485#705485
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Unauthorized", new { ReturnUrl = Request.UrlReferrer });

            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.Username, model.Password))
                {
                    FormsService.SignIn(model.Username, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewBag.MaxPasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Kudos: http://www.dotnetcurry.com/ShowArticle.aspx?ID=611
                if (ReCaptcha.Validate(privateKey: 
                    ConfigurationManager.AppSettings["ReCaptcha_PrivateKey"]))
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus = MembershipService.CreateUser(model.Username, model.Password, model.Email);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        FormsService.SignIn(model.Username, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.MaxPasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult IsUsernameAvailable(string username)
        {
            return Json(MembershipService.IsUsernameAvailable(username));
        }


        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Unauthorized(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

    }
}
