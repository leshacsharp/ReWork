using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ReWork.Common;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IUserService _userService;

        public ManageController(IUserService userService)
        {
            _userService = userService;
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

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changeModel)
        {
            if (!ModelState.IsValid)
                return View(changeModel);

            ChangePasswordParams changeParam = Mapping<ChangePasswordViewModel, ChangePasswordParams>.MapObject(changeModel);
            changeParam.UserName = User.Identity.Name;
            IdentityResult changeResult = _userService.ChangePassword(changeParam);

            if (changeResult.Succeeded)
            {
                LoginViewModel loginParams = new LoginViewModel() { UserName = User.Identity.Name, Password = changeModel.NewPassword, RememberMe = true };
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Account");
            }

            AddModeErrors(changeResult);
            return View(changeModel);
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