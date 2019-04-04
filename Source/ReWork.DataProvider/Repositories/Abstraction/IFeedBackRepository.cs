using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IFeedBackRepository : IRepository<FeedBack>
    {
        IEnumerable<FeedBack> FindFeedBacksByUserName(string userName);
    }
}
