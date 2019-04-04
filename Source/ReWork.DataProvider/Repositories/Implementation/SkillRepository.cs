using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class SkillRepository : ISkillRepository
    {
        private IDbContext _db;
        public SkillRepository(IDbContext db)
        {
            _db = db;
        }

        public void Create(Skill item)
        {
            _db.Skills.Add(item);
        }

        public void Delete(Skill item)
        {
            _db.Skills.Remove(item);
        }

        public Skill FindSkillByTitle(string title)
        {
            return _db.Skills.First(p => p.Title.Equals(title));
        }

        public void Update(Skill item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
