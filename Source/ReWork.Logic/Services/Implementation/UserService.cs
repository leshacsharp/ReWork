using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Infustructure;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels.Account;
using System;
using System.Security.Claims;

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

        public OperationDetails Create(RegisterViewModel reg, string roleName)
        {         
            User user =  _userManager.FindByName(reg.UserName);
            if(user == null)
            {
                User newUser = new User() { UserName = reg.UserName, Email = reg.Email, RegistrationdDate = DateTime.Now };

                IdentityResult regResult =  _userManager.Create(newUser, reg.Password);
                if (!regResult.Succeeded)
                    return new OperationDetails(false, "", String.Join(";", regResult.Errors));

                IdentityResult addRoleResult = _userManager.AddToRole(newUser.Id, roleName);
                if(!addRoleResult.Succeeded)
                    return new OperationDetails(false, "", String.Join(";", regResult.Errors));

                _commitProvider.SaveChanges();
                return new OperationDetails(true, "", "User successfully registered");
            }

            return new OperationDetails(false, "UserName", "User with such a UserName exists");
        }

        public ClaimsIdentity Authenticate(LoginViewModel login)
        {
            ClaimsIdentity claims = null;
            User user = _userManager.Find(login.UserName, login.Password);
            if(user != null)
            {
                claims = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claims;
        }
    }
}
