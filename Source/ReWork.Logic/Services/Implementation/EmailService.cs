using Microsoft.AspNet.Identity;
using ReWork.Logic.Infustructure;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Implementation
{
    public class EmailService : ISendMessageService<EmailMessage>
    {
        static private List<MessageWrapper<EmailMessage>> _messages;
        private UserManager<User> _userManager;

        static EmailService()
        {
            _messages = new List<MessageWrapper<EmailMessage>>();
        }

        public EmailService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public void AddMessage(EmailMessage msg)
        {
            var messageWrapper = new MessageWrapper<EmailMessage>(msg);
            _messages.Add(messageWrapper);
        }

        public async Task Send(EmailMessage msg)
        {
            var messageWrapper = _messages.FirstOrDefault(m => m.Data == msg);
            if (messageWrapper == null)
                throw new ObjectNotFoundException($"Message with id={msg.Id} not found in list, add messages before sending");

            try
            {
                if (DateTime.Now >= messageWrapper.DateNextSending)
                {
                    messageWrapper.AttemptsCount++;
                    await _userManager.SendEmailAsync(msg.UserId, msg.Subject, msg.Body);
                    messageWrapper.Status = MessageStatus.Sended;
                }
            }

            catch (SmtpException ex)
            {
                messageWrapper.Status = MessageStatus.FaildSend;
            }
        }

        public async Task SendAll()
        {
            for (int i = 0; i < _messages.Count; i++)
            {
                if (_messages[i].Status == MessageStatus.Sended || _messages[i].AttemptsCount == 6)
                {
                    _messages.Remove(_messages[i]);
                    i--;
                }

                await Send(_messages[i].Data);
            }       
        }
    }
}
