using Microsoft.AspNet.Identity;
using ReWork.Model.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IUserService 
    {
        IdentityResult Create(string userName, string email, string password, string role);
        ClaimsIdentity Authenticate(string userName, string password);


        IdentityResult ChangePassword(string userId, string oldPassword, string newPassword);
        void ResetPassword(string email);
        IdentityResult ConfirmResetPassword(string userId,string newPassword, string token);


        void EmailConfirmed(string userId, string callbackUrl);
        IdentityResult ConfirmEmail(string userId, string token);
        bool IsEmailConfirmed(string userId);


        User FindUserByName(string userName);
        User FindUserById(string userId);
        IEnumerable<User> GetNewUsers(int page, int usersCount);


        void DeleteUser(string userId);
        void EditUser(string userId, IEnumerable<string> roles);

        IEnumerable<string> GetUserRoles(string userId);
        bool UserNameExists(string userName);
    }
}
