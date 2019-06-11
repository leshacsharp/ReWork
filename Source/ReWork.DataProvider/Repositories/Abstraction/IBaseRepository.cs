using ReWork.Model.Context;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IBaseRepository
    {
        IDbContext Db { get; set; }
    }
}
