using Microsoft.AspNet.Identity;
using ReWork.Common;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using System;
using System.Security.Claims;
using System.Web;

namespace ReWork.Logic.Services.Implementation
{
    public class UserService : IUserService
    {
        private ICommitProvider _commitProvider;
        private UserManager<User> _userManager;

        public UserService(ICommitProvider commitProvider, UserManager<User> userManager)
        {
            _commitProvider = commitProvider;
            _userManager = userManager;
        }

        public IdentityResult Create(RegisterParams reg)
        {         
            User user = _userManager.FindByName(reg.UserName);
            if(user == null)
            {
                User newUser = Mapping<RegisterParams, User>.MapObject(reg);
                newUser.RegistrationdDate = DateTime.Now;

                IdentityResult regResult =  _userManager.Create(newUser, reg.Password);
                if (!regResult.Succeeded)
                    return regResult;

                IdentityResult addRoleResult = _userManager.AddToRole(newUser.Id, reg.Role);
                if (!addRoleResult.Succeeded)
                    return addRoleResult;

                _commitProvider.SaveChanges();
                return regResult;
            }

            return new IdentityResult("User with such a UserName exists");
        }

        public ClaimsIdentity Authenticate(LoginParams login)
        {
            ClaimsIdentity claims = null;
            User user = _userManager.Find(login.UserName, login.Password);
            if(user != null)
            {
                claims = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claims;
        }

        public IdentityResult ChangePassword(ChangePasswordParams changeModel)
        {
            User user = _userManager.FindByName(changeModel.UserName);
            if (user != null) 
            {
                IdentityResult changeRes = _userManager.ChangePassword(user.Id, changeModel.OldPassword, changeModel.NewPassword);
                return changeRes;
            }

            return new IdentityResult("user with such a UserName not found");
        }


        public bool UserNameExists(string userName)
        {
            User user = _userManager.FindByName(userName);
            return user != null;
        }

        public User FindUserByName(string userName)
        {
            return _userManager.FindByName(userName);
        }



        public void ResetPassword(string email)
        {
            User user = _userManager.FindByEmail(email);
            if(user != null)
            {
                string token = _userManager.GeneratePasswordResetToken(user.Id);
                _userManager.SendEmail(user.Id, "Reset password", $"Your token:{token}");
            }
        }

        public IdentityResult ConfirmResetPassword(string userId,string newPassword, string token)
        {
            return _userManager.ResetPassword(userId, token, newPassword);
        }



        public void EmailConfirmed(string userId, string callbackUrl)
        {
            User user = _userManager.FindById(userId);
            if (user != null)
            {
                string token = _userManager.GenerateEmailConfirmationToken(userId);
                string encodedToken = HttpUtility.UrlEncode(token);
                string changedCallbackUrl = $"{callbackUrl}?userId={userId}&token={encodedToken}";
                _userManager.SendEmail(userId, "Confirm your account", $"Please confirm your account by clicking <a href=\"{changedCallbackUrl}\">here</a>");
            }
        }

        public IdentityResult ConfirmEmail(string userId, string token)
        {
            return _userManager.ConfirmEmail(userId, token);
        }

        public bool IsEmailConfirmed(string userId)
        {
            return _userManager.IsEmailConfirmed(userId);
        }
    }
}
