using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IFeedBackService
    {
        IEnumerable<FeedBack> FindFeedBacksForCustomer(string customerId);
        IEnumerable<FeedBack> FindFeedBacksForEmployee(string employeeId);
    }
}
