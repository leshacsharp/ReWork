using Microsoft.Owin.Security;
using ReWork.Common;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model;
using ReWork.Model.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }


        [HttpGet]
        public ActionResult registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registration(RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
                return View(registerModel);

            _userService.Create(registerModel, "user");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {  
                ClaimsIdentity claims = _userService.Authenticate(loginModel);

                if (claims != null)
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = loginModel.RememberMe
                    }, claims);

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "UserName or Password not valid");
                }
            }

            return View(loginModel);
        }


        [HttpGet]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}