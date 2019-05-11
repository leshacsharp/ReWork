using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IFeedBackService
    {
        void CreateFeedBack(CreateFeedBackParams createParams);
        IEnumerable<FeedBackInfo> FindSentFeedBacks(string reciverId);
        IEnumerable<FeedBackInfo> FindRecivedFeedBacks(string senderId);
    }
}
