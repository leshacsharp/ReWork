using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<Offer> FindOffersByUserName(string userName);
    }
}
