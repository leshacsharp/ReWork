using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using System.Security.Claims;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IUserService 
    {
        IdentityResult Create(RegisterParams reg);
        ClaimsIdentity Authenticate(LoginParams login);

        IdentityResult ChangePassword(ChangePasswordParams changeModel);

        User FindUserByName(string userName);
        bool UserNameExists(string userName);

        void ResetPassword(string email);
        IdentityResult ConfirmResetPassword(string userId,string newPassword, string token);


        void EmailConfirmed(string userId, string callbackUrl);
        IdentityResult ConfirmEmail(string userId, string code);
        bool IsEmailConfirmed(string userId);
    }
}
