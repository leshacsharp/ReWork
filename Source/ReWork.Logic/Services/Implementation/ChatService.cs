using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Hubs.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Implementation
{
    public class ChatService : IChatService
    {
        private IChatRoomRepository _chatRoomRepository;
        private IMessageRepository _messageRepository;
        private IChatHub _chatHub;
        private UserManager<User> _userManager;

        public ChatService(IChatRoomRepository chatRoomRepository, IMessageRepository messageRepository, IChatHub chatHub, UserManager<User> userManager)
        {
            _chatRoomRepository = chatRoomRepository;
            _messageRepository = messageRepository;
            _chatHub = chatHub;
            _userManager = userManager;
        }

        public void CreateChatRoom(string title, IEnumerable<string> usersId)
        {
            var chatRoom = new ChatRoom() { Title = title };

            foreach (var id in usersId)
            {
                var user = _userManager.FindById(id);
                if (user == null)
                    throw new ObjectNotFoundException($"User with id={id} not found");

                chatRoom.Users.Add(user);
            }

            _chatRoomRepository.Create(chatRoom);
        }

        public void DeleteChatRoom(int id)
        {
            var chatRoom = _chatRoomRepository.FindById(id);
            if(chatRoom == null)
                throw new ObjectNotFoundException($"ChatRoom with id={id} not found");

            _chatRoomRepository.Delete(chatRoom);
        }


        public IEnumerable<MessageInfo> FindMessages(int chatRoomId)
        {
            return _messageRepository.FindMessageInfo(p => p.ChatRoomId == chatRoomId).ToList();
        }


        public void RefreshChatRoom(int chatRoomId)
        {
            var newMsg = _messageRepository.FindMessageInfo(p => p.ChatRoomId == chatRoomId).First();

            var newMsgModel = new MessageViewModel()
            {
                Text = newMsg.Text,
                DateAdded = newMsg.DateAdded,
                SenderId = newMsg.SenderId,
                SenderName = newMsg.SenderName,
                SenderImagePath = Convert.ToBase64String(newMsg.SenderImage)
            };

            string newMsgJSON = JsonConvert.SerializeObject(newMsgModel);

            _chatHub.RefreshChatRoom(chatRoomId, newMsgJSON);
        }


        public void CreateMessage(string senderId, int chatRoomId, string text)
        {
            var chatRoom = _chatRoomRepository.FindById(chatRoomId);
            if(chatRoom == null)
                throw new ObjectNotFoundException($"ChatRoom with id={chatRoomId} not found");

            var sender = _userManager.FindById(senderId);
            if(sender == null)
                throw new ObjectNotFoundException($"User with id={senderId} not found");

            var message = new Message()
            {
                Text = text,
                DateAdded = DateTime.UtcNow,
                Sender = sender,
                ChatRoom = chatRoom
            };

            _messageRepository.Create(message);
        }
    }
}
