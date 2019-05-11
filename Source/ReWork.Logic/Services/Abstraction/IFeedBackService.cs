using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IFeedBackService
    {
        IEnumerable<FeedBackInfo> FindSentFeedBacks(string recivedId);
        IEnumerable<FeedBackInfo> FindRecivedFeedBacks(string senderId);
    }
}
