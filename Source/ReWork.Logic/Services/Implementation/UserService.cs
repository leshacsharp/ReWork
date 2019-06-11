using FirstQuad.Common.Helpers;
using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ReWork.Logic.Services.Implementation
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private ICustomerProfileRepository _customerRep;
        private IEmployeeProfileRepository _employeeRep;

        public UserService(UserManager<User> userManager, ICustomerProfileRepository customerRep, IEmployeeProfileRepository employeeRep)
        { 
            _userManager = userManager;
            _customerRep = customerRep;
            _employeeRep = employeeRep;
        }

        public IdentityResult Create(string userName, string email, string password, string role)
        {         
            var user = _userManager.FindByName(userName);

            if(user == null)
            {
                string defaultImagePath = HttpContext.Current.Server.MapPath("~/Content/cube-512.png");
                byte[] imageBytes = System.IO.File.ReadAllBytes(defaultImagePath);

                var newUser = new User()
                { UserName = userName, Email = email, RegistrationdDate = DateTime.Now, Image = imageBytes };

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
            var user = _userManager.Find(userName, password);
            if(user != null)
            {
                claims = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claims;
        }


        public IdentityResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = _userManager.FindById(userId);
            if (user != null)
            {
                IdentityResult changeRes = _userManager.ChangePassword(user.Id, oldPassword, newPassword); 
                return changeRes;
            }

            return new IdentityResult("user with such a UserName not found");
        }

        public void ResetPassword(string email)
        {
            var user = _userManager.FindByEmail(email);
            if(user != null)
            {
                string token = _userManager.GeneratePasswordResetToken(user.Id);
                _userManager.SendEmail(user.Id, "Reset password", $"Token for reset password:  {token}");
            }
        }

        public IdentityResult ConfirmResetPassword(string email, string newPassword, string token)
        {
            var user = _userManager.FindByEmail(email);
            if (user != null)
            {
                return _userManager.ResetPassword(user.Id, token, newPassword);
            }
            return new IdentityResult("User with a such email not found");
        }
           

        public void EmailConfirmed(string userId, string callbackUrl)
        {
            var user = _userManager.FindById(userId);
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

        public IEnumerable<UserInfo> FindUsersInfo(string userName)
        {
            var filter = PredicateBuilder.True<User>();

            if(!String.IsNullOrEmpty(userName))
            {
                filter = filter.AndAlso(u => u.UserName.Contains(userName));
            }

            return (from u in _userManager.Users.Where(filter)
                    orderby u.RegistrationdDate descending
                    select new UserInfo()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Email = u.Email,
                        Image = u.Image
                    }).ToList();
        }


        public void DeleteUser(string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");


            if (user.CustomerProfile != null)
                _customerRep.Delete(user.CustomerProfile);

            if (user.EmployeeProfile != null)
                _employeeRep.Delete(user.EmployeeProfile);

            _userManager.Delete(user);
        }

        public void EditUserRoles(string userId, IEnumerable<string> roles)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            var currentUserRoles = _userManager.GetRoles(user.Id);
            _userManager.RemoveFromRoles(user.Id, currentUserRoles.ToArray());

            foreach (var role in roles)
            {
                _userManager.AddToRole(userId, role);
            }
        }

        public void EditUser(string userId, string firstName, string lastName)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            user.FirstName = firstName;
            user.LastName = lastName;

            _userManager.Update(user); 
        }

        public void UploadImage(string userId, byte[] image)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            user.Image = image;
            _userManager.Update(user);
        }


        public IEnumerable<string> GetUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            return _userManager.GetRoles(userId);
        }

        public bool UserNameExists(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user != null;
        }

        public int UsersCount()
        {
            return _userManager.Users.Count();
        }
    }
}
