using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IFeedBackRepository : IRepository<FeedBack>
    {
        IEnumerable<FeedBackInfo> FindSentFeedBacks(string senderId);
        IEnumerable<FeedBackInfo> FindRecivedFeedBacks(string reciverId);
    }
}
