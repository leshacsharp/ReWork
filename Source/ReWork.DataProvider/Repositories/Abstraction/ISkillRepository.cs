using ReWork.DataProvider.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISkillRepository : IRepository<Skill>
    {
        IEnumerable<Skill> FindSkillByTitle(string title);
    }
}
