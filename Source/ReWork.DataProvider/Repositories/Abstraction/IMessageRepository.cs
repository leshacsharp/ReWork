using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IMessageRepository : IRepository<Message>
    {
        IQueryable<MessageInfo> FindMessageInfo(int chatRoomId);
    }
}
