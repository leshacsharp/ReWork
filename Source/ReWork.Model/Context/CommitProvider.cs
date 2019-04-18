using ReWork.Model.Context;

namespace ReWork.Model.Context
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
