using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IMessageRepository : IRepository<Message>
    {
        IQueryable<MessageInfo> FindMessageInfo(int chatRoomId);
    }
}
