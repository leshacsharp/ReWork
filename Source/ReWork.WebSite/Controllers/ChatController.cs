using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private IChatService _chatService;
        private ICommitProvider _commitProvider;

        public ChatController(IChatService chatService, ICommitProvider commitProvider)
        {
            _chatService = chatService;
            _commitProvider = commitProvider;
        }

        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public void AddMessage(string text)
        {
            string senderId = User.Identity.GetUserId();

            _chatService.CreateMessage(senderId, 1, text);
            _commitProvider.SaveChanges();

            _chatService.RefreshChatRoom(1);
        }
    }
}