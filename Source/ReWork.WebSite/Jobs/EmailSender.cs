using Quartz;
using ReWork.Logic.Infustructure;
using ReWork.Logic.Services.Abstraction;
using System.Threading.Tasks;

namespace ReWork.WebSite.Jobs
{
    public class EmailSender : IJob
    {
        ISendMessageService<EmailMessage> _emailService;
      
        public EmailSender(ISendMessageService<EmailMessage> emailService)
        {
            _emailService = emailService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return _emailService.SendAll();
        }
    }
}