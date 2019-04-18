using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ReWork.Logic.Infustructure;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels.Account;
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
        public ActionResult registration(RegisterViewModel regModel)
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
                User user =_userService.FindUserByName(regModel.UserName);
                string callbackUrl = Url.Action("ConfirmEmail", "account", null, Request.Url.Scheme);
                _userService.EmailConfirmed(user.Id, callbackUrl);

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

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claims = _userService.Authenticate(loginModel.UserName, loginModel.Password);

                if (claims != null)
                {
                    User user = _userService.FindUserByName(loginModel.UserName);
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


        [Authorize]
        [HttpGet]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }
    }
}