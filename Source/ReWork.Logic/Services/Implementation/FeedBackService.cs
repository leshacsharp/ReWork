using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Implementation
{
    public class FeedBackService : IFeedBackService
    {
        private IFeedBackRepository _feedBackRepository;

        public FeedBackService(IFeedBackRepository feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }

        public IEnumerable<FeedBackInfo> FindRecivedFeedBacks(string recivedId)
        {
            return _feedBackRepository.FindRecivedFeedBacks(recivedId);
        }

        public IEnumerable<FeedBackInfo> FindSentFeedBacks(string senderId)
        {
            return _feedBackRepository.FindSentFeedBacks(senderId);
        }
    }
}
