using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private IUserService _userService;

        public SettingsController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult General()
        {
            string userId = User.Identity.GetUserId();
            User user = _userService.FindUserById(userId);
            return View(user);
        }

        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changeModel)
        {
            if (!ModelState.IsValid)
                return View(changeModel);

            string userId = User.Identity.GetUserId();
            IdentityResult changeResult = _userService.ChangePassword(userId, changeModel.OldPassword, changeModel.NewPassword);

            if (changeResult.Succeeded)
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Account");
            }

            AddModeErrors(changeResult);
            return View(changeModel);
        }





        [HttpPost]
        public void SetProfileOnCustomer()
        {
            Response.Cookies["profile"].Value = "customer";
        }

        [HttpPost]
        public void SetProfileOnEmployee()
        {
            Response.Cookies["profile"].Value = "employee";
        }


        private void AddModeErrors(IdentityResult result)
        {
            foreach (var msg in result.Errors)
            {
                ModelState.AddModelError("", msg);
            }
        }
    }
}