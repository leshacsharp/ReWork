using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.ViewModels.Chat;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private IChatRoomService _chatRoomService;
        private IMessageService _messageService;
        private IUserService _userService;
        private INotificationService _notificationService;
        private ICommitProvider _commitProvider;

        public ChatController(IChatRoomService chatRoomService, IMessageService messageService, IUserService userService, INotificationService notificationService, ICommitProvider commitProvider)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            _userService = userService;
            _notificationService = notificationService;
            _commitProvider = commitProvider;
        }

        [HttpGet]
        public ActionResult ChatRooms()
        {
            string userId = User.Identity.GetUserId();
            var chatRooms = _chatRoomService.FindChatRooms(userId);

            var chatRoomModels = from ch in chatRooms
                                 select new ChatRoomViewModel()
                                 {
                                     Id = ch.Id,
                                     Title = ch.Title,
                                     LastMessage = ch.LastMessage?.Text,
                                     LastMessageDate = ch.LastMessage?.DateAdded,
                                     Users = (from u in ch.Users
                                              select new UserViewModel()
                                              {
                                                  Id = u.Id,
                                                  UserName = u.UserName,
                                                  ImagePath = Convert.ToBase64String(u.Image)
                                              })
                                 };

            return View("ChatRooms", chatRoomModels);
        }
        
        [HttpGet]
        public ActionResult Room(int id)
        {
            var chatRoom = _chatRoomService.FindChatRoom(id);

            var charRoomModel = new ChatRoomDetailsViewModel()
            {
                Id = chatRoom.Id,
                Title = chatRoom.Title,
                Users = (from u in chatRoom.Users
                         select new UserViewModel()
                         {
                             Id = u.Id,
                             UserName = u.UserName,
                             ImagePath = Convert.ToBase64String(u.Image)
                         })
            };

            return View(charRoomModel);
        }

        [HttpPost]
        public ActionResult CreateChatRoom(string userId)
        {
            string currentUserId = User.Identity.GetUserId();
            string[] usersId = new string[] { currentUserId, userId };

            _chatRoomService.CreateChatRoom(usersId);
            _notificationService.CreateNotification(currentUserId, userId, "You invited to chat room");
            _commitProvider.SaveChanges();

            _notificationService.RefreshNotifications(userId);
            return ChatRooms();
        }

        [HttpPost]
        public ActionResult DeleteMemberFromChatRoom(int chatRoomId)
        {
            string userId = User.Identity.GetUserId();

            _chatRoomService.DeleteMemberFromChatRoom(chatRoomId, userId);
            _commitProvider.SaveChanges();

            return ChatRooms();
        }

        [HttpPost]
        public void AddMemberToChatRoom(int chatRoomId, string userId)
        {
            string currentUserId = User.Identity.GetUserId();

            _chatRoomService.AddMemberToChatRoom(chatRoomId, userId);
            _notificationService.CreateNotification(currentUserId, userId, "You invited to chat room");

            _commitProvider.SaveChanges();
            _notificationService.RefreshNotifications(userId);
        }

        [HttpGet]
        public ActionResult EditChatRoom(int id)
        {
            var editModel = new EditRoomViewModel() { ChatRoomId = id };
            return PartialView(editModel);
        }

        [HttpPost]
        public ActionResult EditChatRoom(EditRoomViewModel editModel)
        {
            if (!ModelState.IsValid)
                return PartialView(editModel);

            _chatRoomService.EditChatRoom(editModel.ChatRoomId, editModel.NewTitle);
            _commitProvider.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [HttpPost]
        public ActionResult FindMessages(int chatRoomId, int count, int page = 1)
        {
            var messages = _messageService.FindMessages(chatRoomId, page, count);

            var messageModels = from m in messages
                                select new MessageViewModel()
                                {
                                    Text = m.Text,
                                    DateAdded = m.DateAdded,
                                    SenderId = m.SenderId,
                                    SenderName = m.SenderName,
                                    SenderImagePath = Convert.ToBase64String(m.SenderImage)
                                };

            return Json(messageModels);
        }

        [HttpPost]
        public void AddMessage(int chatRoomId, string text)
        {
            string senderId = User.Identity.GetUserId();

            _messageService.CreateMessage(senderId, chatRoomId, text);
            _commitProvider.SaveChanges();

            _chatRoomService.RefreshChatRoom(chatRoomId);
        }


        [HttpPost]
        public ActionResult FindUsers(string userName)
        {
            var users = _userService.FindUsersInfo(userName);

            var userModels = from u in users
                             select new UserViewModel()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 ImagePath = Convert.ToBase64String(u.Image)
                             };

            return new JsonResult() { Data = userModels, MaxJsonLength = int.MaxValue };
        }
    }
}