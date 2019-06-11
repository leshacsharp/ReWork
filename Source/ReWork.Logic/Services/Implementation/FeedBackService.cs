using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;

namespace ReWork.Logic.Services.Implementation
{
    public class FeedBackService : IFeedBackService
    {
        private IFeedBackRepository _feedBackRepository;
        private UserManager<User> _userManager;
        private IJobRepository _jobRepository;

        public FeedBackService(IFeedBackRepository feedBackRepository, UserManager<User> userManager, IJobRepository jobRepository)
        {
            _feedBackRepository = feedBackRepository;
            _userManager = userManager;
            _jobRepository = jobRepository;
        }

        public void CreateFeedBack(CreateFeedBackParams createParams)
        {
            var sender = _userManager.FindById(createParams.SenderId);
            if (sender == null)
                throw new ObjectNotFoundException($"Sender with id={createParams.SenderId} not found");

            var reciver = _userManager.FindById(createParams.ReciverId);
            if (reciver == null)
                throw new ObjectNotFoundException($"Reciver with id={createParams.ReciverId} not found");


            var job = _jobRepository.FindJobById(createParams.JobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={createParams.JobId} not found");
            

            var feedBack = new FeedBack()
            {
                Sender = sender,
                Receiver = reciver,
                Job = job,
                Text = createParams.Text,
                QualityOfWork = createParams.QualityOfWork,
                AddedDate = DateTime.UtcNow
            };

            _feedBackRepository.Create(feedBack); 
        }

        public IEnumerable<FeedBackInfo> FindRecivedFeedBacks(string reciverId)
        {
            return _feedBackRepository.FindRecivedFeedBacks(reciverId);
        }

        public IEnumerable<FeedBackInfo> FindSentFeedBacks(string senderId)
        {
            return _feedBackRepository.FindSentFeedBacks(senderId);
        }
    }
}
