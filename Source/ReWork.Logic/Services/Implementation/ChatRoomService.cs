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

namespace ReWork.Logic.Services.Implementation
{
    public class ChatRoomService : IChatRoomService
    {
        private IChatRoomRepository _chatRoomRepository;
        private IMessageRepository _messageRepository;
        private IChatHub _chatHub;
        private UserManager<User> _userManager;

        public ChatRoomService(IChatRoomRepository chatRoomRepository, IMessageRepository messageRepository, IChatHub chatHub, UserManager<User> userManager)
        {
            _chatRoomRepository = chatRoomRepository;
            _messageRepository = messageRepository;
            _chatHub = chatHub;
            _userManager = userManager;
        }

        public void CreateChatRoom(IEnumerable<string> usersId)
        {
            var chatRoom = new ChatRoom();
            var roomTitle = new StringBuilder();

            foreach (var id in usersId)
            {
                var user = _userManager.FindById(id);
                if (user == null)
                    throw new ObjectNotFoundException($"User with id={id} not found");

                roomTitle.Append($" - {user.UserName}");
                chatRoom.Users.Add(user);
            }

            chatRoom.Title = roomTitle.ToString();
            _chatRoomRepository.Create(chatRoom);
        }

        public void DeleteMemberFromChatRoom(int chatRoomId, string userId)
        {
            var chatRoom = _chatRoomRepository.FindById(chatRoomId);
            if (chatRoom == null)
                throw new ObjectNotFoundException($"ChatRoom with id={chatRoomId} not found");

            var user = _userManager.FindById(userId);
            if(user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            if (chatRoom.Users.Count > 1)
            {
                chatRoom.Users.Remove(user);
            }
            else
            {
                _chatRoomRepository.Delete(chatRoom);
            }
        }

        public void AddMemberToChatRoom(int chatRoomId, string userId)
        {
            var chatRoom = _chatRoomRepository.FindById(chatRoomId);
            if (chatRoom == null)
                throw new ObjectNotFoundException($"ChatRoom with id={chatRoomId} not found");

            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            chatRoom.Users.Add(user);
        }

        public void EditChatRoom(int chatRoomId, string newTitle)
        {
            var chatRoom = _chatRoomRepository.FindById(chatRoomId);
            if (chatRoom == null)
                throw new ObjectNotFoundException($"ChatRoom with id={chatRoomId} not found");

            chatRoom.Title = newTitle;
            _chatRoomRepository.Update(chatRoom);
        }


        public ChatRoomDetailsInfo FindChatRoom(int chatRoomId)
        {
            return _chatRoomRepository.FindChatRoomInfo(chatRoomId);
        }

        public IEnumerable<ChatRoomInfo> FindChatRooms(string userId)
        {
            return _chatRoomRepository.FindChatRooms(userId);
        }


        public void RefreshChatRoom(int chatRoomId)
        {
            var newMsg = _messageRepository.FindMessageInfo(chatRoomId).FirstOrDefault();

            if (newMsg != null)
            {
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
        }
    }
}
