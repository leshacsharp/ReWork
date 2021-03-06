﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.ViewModels.Account;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private ICustomerProfileService _cusomerService;
        private ICommitProvider _commitProvider;

        public AccountController(IUserService userService, ICustomerProfileService customerService, ICommitProvider commitProvider)
        {
            _userService = userService;
            _cusomerService = customerService;
            _commitProvider = commitProvider;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> registration(RegisterViewModel regModel)
        {
            if (!ModelState.IsValid)
            {
                if (regModel.UserName != null && _userService.UserNameExists(regModel.UserName))
                    ModelState.AddModelError("UserName", "user with such a UserName exists");
                return View(regModel);
            }

            IdentityResult regResult = _userService.Create(regModel.UserName, regModel.Email, regModel.Password, "user");

            if (regResult.Succeeded)
            {
                var user =_userService.FindUserByName(regModel.UserName);
                string callbackUrl = Url.Action("ConfirmEmail", "account", null, Request.Url.Scheme);
                await _userService.EmailConfirmed(user.Id, callbackUrl);

                _cusomerService.CreateCustomerProfile(user.Id);
                _commitProvider.SaveChanges();

                return RedirectToAction("DisplayEmail", "account");
            }

            AddModeErrors(regResult);
            return View(regModel);
        }


        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var claims = _userService.Authenticate(loginModel.UserName, loginModel.Password);

                if (claims != null)
                {
                    var user = _userService.FindUserByName(loginModel.UserName);
                    if (user.EmailConfirmed)
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties()
                        {
                            IsPersistent = loginModel.RememberMe
                        }, claims);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is not confirmed");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password not valid");
                }
            }

            return View(loginModel);
        }


        [HttpGet]
        public ActionResult ConfirmEmail(string id, string token)
        {
            if (id == null || token == null)
            {
                return View("Error");
            }
          
            IdentityResult identityResult = _userService.ConfirmEmail(id, token);
            return View(identityResult.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        public ActionResult DisplayEmail()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordModel)
        {
            if(!ModelState.IsValid)
                return View(forgotPasswordModel);

            _userService.ResetPassword(forgotPasswordModel.Email);
            return View("ResetPassword");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
       
            IdentityResult resetResult = _userService.ConfirmResetPassword(resetPasswordModel.Email, resetPasswordModel.NewPassword,resetPasswordModel.Token);
            if(resetResult.Succeeded)
            {
                return View("ResetPasswordConfirm");
            }

            AddModeErrors(resetResult);
            return View(resetPasswordModel);
        }

        [HttpGet]
        public ActionResult ResetPasswordConfirm()
        { 
            return View();
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


        [Authorize]
        [HttpGet]
        public ActionResult Settings()
        {
            string userId = User.Identity.GetUserId();

            var user = _userService.FindUserById(userId);
            if (user == null)
                return View("Error");


            var editModel = new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ImagePath = Convert.ToBase64String(user.Image)
            };

            return View(editModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Settings(EditUserViewModel editModel)
        {
            if(!ModelState.IsValid )
            {
                return View(editModel);
            }

            _userService.EditUser(editModel.Id, editModel.FirstName, editModel.LastName);
            _commitProvider.SaveChanges();

            return RedirectToAction("Settings","Account");
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            string userId = User.Identity.GetUserId();
            var userPhoto = Request.Files["userphoto[0]"];
            byte[] imageBytes = null;

            if (userPhoto.ContentLength > 0)
            {
                imageBytes = new byte[userPhoto.InputStream.Length];
                using (BinaryReader reader = new BinaryReader(userPhoto.InputStream))
                {
                    reader.Read(imageBytes, 0, imageBytes.Length);
                }
            }

            _userService.UploadImage(userId, imageBytes);
            _commitProvider.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [Authorize]
        [HttpGet]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Jobs", "Job");
        }


        private void AddModeErrors(IdentityResult result)
        {
            foreach (var msg in result.Errors)
            {
                ModelState.AddModelError("", msg);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Jobs", "Job");
        }
    }
}