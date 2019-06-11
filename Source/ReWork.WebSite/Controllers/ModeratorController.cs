using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels.Account;
using System;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize(Roles = "moderator")]
    public class ModeratorController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private ICommitProvider _commitProvider;

        public ModeratorController(IUserService userService, IRoleService roleService, ICommitProvider commitProvider)
        {
            _userService = userService;
            _roleService = roleService;
            _commitProvider = commitProvider;
        }

        [HttpGet]
        public ActionResult Users()
        {   
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult FindUsers(string userName)
        {
            var users = _userService.FindUsersInfo(userName);
            return Json(users);
        }


        [HttpGet]
        public ActionResult DetailsUser(string id)
        {
            var user = _userService.FindUserById(id);
            if (user == null)
                return View("Error");

            var userDetails = new UserDetailsViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RegistrationDate = (DateTime)user.RegistrationdDate,
                Roles = _userService.GetUserRoles(user.Id)
            };

            return PartialView(userDetails);
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            User user = _userService.FindUserById(id);
            if (user == null)
                return View("Error");

            EditUserRolesViewModel editUserModel = new EditUserRolesViewModel()
            { Id = user.Id, UserName = user.UserName };

            editUserModel.AllRoles = _roleService.GetAll();
            editUserModel.UserRoles = _userService.GetUserRoles(user.Id);

            return View(editUserModel);
        }

        [HttpPost]
        public ActionResult EditUser(EditUserRolesViewModel editUserModel)
        {
            _userService.EditUserRoles(editUserModel.Id, editUserModel.UserRoles);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public void DeleteUser(string id)
        {
            _userService.DeleteUser(id);
            _commitProvider.SaveChanges();
        }
    }
}