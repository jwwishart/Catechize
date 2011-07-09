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
using Catechize.Helpers;
using System.Web.Profile;
using System.Threading;

namespace Catechize.Controllers
{
    // TODO: Password Recovery Disabled for master account
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }


        // Constructors
        //

        public AccountController(
            IFormsAuthenticationService formsService)
        {
            this.FormsService = formsService;
        }

        public ActionResult SetupDatabaseDefaults()
        {
            // Only allow this is Dev or S
            if (Settings.AppMode != ApplicationMode.Production)
            {
                if (Membership.GetUser("master") == null)
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
                }

                return this.RedirectToHome();
            }
            else {
                return HttpNotFound();
            }
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
                if (Membership.ValidateUser(model.Username, model.Password))
                {
                    FormsService.SignIn(model.Username, model.RememberMe);

                    // Create Language_Code cookie | Set preferred user culture
                    SetPreferredLanguageCookie(model.Username);

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

        private void SetPreferredLanguageCookie(string username)
        {
            Response.Cookies.Add(
                new HttpCookie("Preferred_Language",
                    DefaultProfile.Create(username, true)
                                  .GetPropertyValue("Preferred_Language")
                                  .ToString()));
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
            ViewBag.MaxPasswordLength = Membership.MinRequiredPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            // TODO: Check for username well-formedness! (MembershipService.IsUsernameWellFormed)
            if (ModelState.IsValid)
            {
                // Kudos: http://www.dotnetcurry.com/ShowArticle.aspx?ID=611
                if (ReCaptcha.Validate(privateKey: 
                    ConfigurationManager.AppSettings["ReCaptcha_PrivateKey"]))
                {
                    // Attempt to register the user
                    MembershipCreateStatus status = new MembershipCreateStatus();

                    Membership.CreateUser(model.Username, model.Password, model.Email, string.Empty, string.Empty, true, out status);

                    if (status == MembershipCreateStatus.Success)
                    {
                        FormsService.SignIn(model.Username, true);

                        // TODO: Want to provide this setting via drop down on page
                        DefaultProfile.Create(model.Username).SetPropertyValue("Preferred_Language",
                            Thread.CurrentThread.CurrentUICulture.Name);
                        
                        SetPreferredLanguageCookie(model.Username);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", AccountValidation.ErrorCodeToString(status));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.MaxPasswordLength = Membership.MinRequiredPasswordLength;
            return View(model);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult IsUsernameAvailable(string username)
        {
            return Json((Membership.GetUser(username) == null));
        }


        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = Membership.MinRequiredPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser user = Membership.GetUser(User.Identity.Name);

                // TODO: Validate password???
                // TODO: Consider this better.
                if (user != null) {
                    user.ChangePassword(model.OldPassword, model.NewPassword);
                    Membership.UpdateUser(user);
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = Membership.MinRequiredPasswordLength;
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
