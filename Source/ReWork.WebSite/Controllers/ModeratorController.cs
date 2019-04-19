using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels;
using ReWork.Model.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize(Roles = "moderator")]
    public class ModeratorController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;

        public ModeratorController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult Users(int? page)
        {
            int usersCountOnPage = 1;
            int pageNumber = page ?? 1; 

            IEnumerable<User> users = _userService.GetNewUsers(pageNumber, usersCountOnPage);

            PageInfo pageInfo = new PageInfo() { CurrentPage = pageNumber, ItemsOnPage = usersCountOnPage };
            pageInfo.TotalItems = _userService.UsersCount();

            UsersViewModel usersModel = new UsersViewModel() { PageInfo = pageInfo, Users = users };
            return View(usersModel);
        }

        [HttpGet]
        public ActionResult Details(string userName)
        {
            User user = _userService.FindUserByName(userName);
            if (user != null)
            {
                UserDetailsViewModel userDetails = new UserDetailsViewModel()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    RegistrationDate = (DateTime)user.RegistrationdDate,
                    Roles = _userService.GetUserRoles(user.Id)
                };

                return View(userDetails);
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult Edit(string userName)
        {
            User user = _userService.FindUserByName(userName);
            if (user != null)
            {
                EditUserViewModel editUserModel = new EditUserViewModel() { Id = user.Id, UserName = user.UserName };
                editUserModel.AllRoles = _roleService.GetAll();
                editUserModel.UserRoles = _userService.GetUserRoles(user.Id);

                return View(editUserModel);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel editUserModel)
        {
            _userService.EditUser(editUserModel.Id, editUserModel.UserRoles);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        [HttpPost]
        public ActionResult Delete(string id)
        {
            _userService.DeleteUser(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}