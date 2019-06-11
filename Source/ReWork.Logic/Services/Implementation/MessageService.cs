using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ReWork.Logic.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private IChatRoomRepository _chatRoomRepository;
        private IMessageRepository _messageRepository;
        private UserManager<User> _userManager;

        public MessageService(IMessageRepository messageRepository, IChatRoomRepository chatRoomRepository, UserManager<User> userManager)
        {
            _messageRepository = messageRepository;
            _chatRoomRepository = chatRoomRepository;
            _userManager = userManager;
        }

        public void CreateMessage(string senderId, int chatRoomId, string text)
        {
            var chatRoom = _chatRoomRepository.FindById(chatRoomId);
            if (chatRoom == null)
                throw new ObjectNotFoundException($"ChatRoom with id={chatRoomId} not found");

            var sender = _userManager.FindById(senderId);
            if (sender == null)
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

        public IEnumerable<MessageInfo> FindMessages(int chatRoomId, int page, int count)
        {
            return _messageRepository.FindMessageInfo(chatRoomId)
                                     .Skip(--page * count)
                                     .Take(count)
                                     .ToList();
        }
    }
}
