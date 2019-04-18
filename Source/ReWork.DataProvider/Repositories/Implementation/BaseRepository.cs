using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class BaseRepository : IBaseRepository
    {
        public IDbContext Db { get; set; }
    }
}
