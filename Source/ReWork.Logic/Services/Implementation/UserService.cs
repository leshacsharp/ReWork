using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ReWork.Logic.Services.Implementation
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        { 
            _userManager = userManager;
        }

        public IdentityResult Create(string userName, string email, string password, string role)
        {         
            User user = _userManager.FindByName(userName);
            if(user == null)
            {
                User newUser = new User() { UserName = userName, Email = email, RegistrationdDate = DateTime.Now };

                IdentityResult regResult =  _userManager.Create(newUser, password);
                if (!regResult.Succeeded)
                    return regResult;

                IdentityResult addRoleResult = _userManager.AddToRole(newUser.Id, role);
                if (!addRoleResult.Succeeded)
                    return addRoleResult;

                return regResult;
            }

            return new IdentityResult("User with such a UserName exists");
        }

        public ClaimsIdentity Authenticate(string userName, string password)
        {
            ClaimsIdentity claims = null;
            User user = _userManager.Find(userName, password);
            if(user != null)
            {
                claims = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claims;
        }



        public IdentityResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            User user = _userManager.FindById(userId);
            if (user != null)
            {
                IdentityResult changeRes = _userManager.ChangePassword(user.Id, oldPassword, newPassword); 
                return changeRes;
            }

            return new IdentityResult("user with such a UserName not found");
        }

        public void ResetPassword(string email)
        {
            User user = _userManager.FindByEmail(email);
            if(user != null)
            {
                string token = _userManager.GeneratePasswordResetToken(user.Id);
                _userManager.SendEmail(user.Id, "Reset password", $"Token for reset password:  {token}");
            }
        }

        public IdentityResult ConfirmResetPassword(string email, string newPassword, string token)
        {
            User user = _userManager.FindByEmail(email);
            if (user != null)
            {
                return _userManager.ResetPassword(user.Id, token, newPassword);
            }
            return new IdentityResult("User with a such email not found");
        }
           



        public void EmailConfirmed(string userId, string callbackUrl)
        {
            User user = _userManager.FindById(userId);
            if (user != null)
            {
                string token = _userManager.GenerateEmailConfirmationToken(userId);
                string encodedToken = HttpUtility.UrlEncode(token);
                string changedCallbackUrl = $"{callbackUrl}?id={userId}&token={encodedToken}";
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




       

        public User FindUserByName(string userName)
        {
            return _userManager.FindByName(userName);
        }

        public User FindUserById(string userId)
        {
            return _userManager.FindById(userId);
        }

        public IEnumerable<User> FindUsers()
        {
            return _userManager.Users
                               .OrderByDescending(p=>p.RegistrationdDate)                 
                               .ToList();
        }



        public void DeleteUser(string userId)
        {
            User user = _userManager.FindById(userId);
            if(user != null)
            {
                _userManager.Delete(user);
            }
        }

        public void EditUser(string userId, IEnumerable<string> roles)
        {
            User user = _userManager.FindById(userId);
            if(user != null)
            {
                IEnumerable<string> currentUserRoles = _userManager.GetRoles(user.Id);             
                _userManager.RemoveFromRoles(user.Id, currentUserRoles.ToArray());

                foreach (var role in roles)
                {
                    _userManager.AddToRole(userId, role);
                }
            }
        }


        public IEnumerable<string> GetUserRoles(string userId)
        {
            User user = _userManager.FindById(userId);
            if(user != null)
            {
                return _userManager.GetRoles(userId);
            }
            return null;
        }

        public bool UserNameExists(string userName)
        {
            User user = _userManager.FindByName(userName);
            return user != null;
        }

        public int UsersCount()
        {
            return _userManager.Users.Count();
        }
    }
}
