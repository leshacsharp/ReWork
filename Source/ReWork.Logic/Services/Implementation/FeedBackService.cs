using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
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

        public IEnumerable<FeedBack> FindFeedBacksForCustomer(string customerId)
        {
            return _feedBackRepository.FindFeedBacksForCustomer(customerId);
        }

        public IEnumerable<FeedBack> FindFeedBacksForEmployee(string employeeId)
        {
            return _feedBackRepository.FindFeedBacksForEmployee(employeeId);
        }
    }
}
