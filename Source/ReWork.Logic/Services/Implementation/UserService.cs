using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.UnitOfWork;
using ReWork.Logic.Dto;
using ReWork.Logic.Infustructure;
using ReWork.Logic.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ReWork.Logic.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUnitOfWork _db;
        public UserService(IUnitOfWork db)
        {
            _db = db;
        }

        public OperationDetails Create(UserDto userDto)
        {
            if (userDto.UserName == null)
                throw new ArgumentNullException("userName", "userName not can be null");

            User user =  _db.UserManager.FindByName(userDto.UserName);
            if(user == null)
            {
                User newUser = new User() { UserName = userDto.UserName, Email = userDto.Email, RegistrationdDate = DateTime.Now };

                IdentityResult regResult =  _db.UserManager.Create(newUser, userDto.Password);
                if (!regResult.Succeeded)
                    return new OperationDetails(false, "", String.Join(";", regResult.Errors));

                IdentityResult addRoleResult = _db.UserManager.AddToRole(newUser.Id, userDto.Role);
                if(!addRoleResult.Succeeded)
                    return new OperationDetails(false, "", String.Join(";", regResult.Errors));

                _db.SaveChanges();
                return new OperationDetails(true, "", "User successfully registered");
            }

            return new OperationDetails(false, "UserName", "User with such a UserName exists");
        }

        public ClaimsIdentity Authenticate(UserDto userDto)
        {
            ClaimsIdentity claims = null;
            User user = _db.UserManager.FindByName(userDto.UserName);
            if(user != null)
            {
                claims = _db.UserManager.CreateIdentity(user,"");
            }

            return claims;
        }

        public void SetInitial(UserDto userDto, IEnumerable<string> roles)
        {
            foreach (var roleName in roles)
            {
                bool roleExists = _db.RoleManager.RoleExists(roleName);
                if (!roleExists)
                {
                    Role role = new Role() { Name = roleName };
                    _db.RoleManager.Create(role);
                }
            }

            Create(userDto);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
