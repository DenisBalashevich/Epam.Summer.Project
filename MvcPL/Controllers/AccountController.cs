using System;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using BLL.Interfaces.Interfaces;
using MvcPL.ViewModels;
using MvcPL.Infastructure;
using MvcPL.Providers;
using System.Drawing.Imaging;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : ErrorController
    {
        private readonly IUserService _repository;

        public AccountController(IUserService repository)
        {
            this._repository = repository;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    try
                    {
                        FormsAuthentication.SetAuthCookie(_repository.GetUserByEmail(viewModel.Email).Name,
                            viewModel.RememberMe);
                    }
                    catch 
                    {
                        ModelState.AddModelError("", "User not found");
                    }
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
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return View(viewModel);
            }

            var anyUser = _repository.GetAll().Any(u => u.Name.Contains(viewModel.Name));

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = null;

                membershipUser = ((CustomMembershipProvider)Membership.Provider)
                   .CreateUser(viewModel.Name, viewModel.Email, viewModel.Password);


                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Name, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            
            ci.Dispose();
            return null;
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
    }
}