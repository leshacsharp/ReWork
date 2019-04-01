using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class SkillRepository : ISkillRepository
    {
        private ReWorkContext _db;
        public SkillRepository(ReWorkContext db)
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

        public IEnumerable<Skill> FindSkillByTitle(string title)
        {
            return _db.Skills.Where(p => p.Title.Equals(title));
        }

        public void Update(Skill item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
