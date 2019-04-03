using ReWork.Logic.Dto;
using ReWork.Logic.Infustructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IUserService : IDisposable
    {
        OperationDetails Create(UserDto userDto);
        ClaimsIdentity Authenticate(UserDto userDto);  
    }
}
