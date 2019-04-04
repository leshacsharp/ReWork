using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class CommitProvider : ICommitProvider
    {
        private IDbContext _db;
        public CommitProvider(IDbContext db)
        {
            _db = db;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
