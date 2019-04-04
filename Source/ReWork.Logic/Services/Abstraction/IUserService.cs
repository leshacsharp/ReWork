using ReWork.Logic.Infustructure;
using ReWork.Model.ViewModels.Account;
using System;
using System.Security.Claims;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IUserService 
    {
        OperationDetails Create(RegisterViewModel registerModel, string roleName);
        ClaimsIdentity Authenticate(LoginViewModel loginModel);  
    }
}
