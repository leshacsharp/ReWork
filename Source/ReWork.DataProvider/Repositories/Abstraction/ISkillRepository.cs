using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Skill FindSkillByTitle(string title);
        Skill FindById(int id);
        IEnumerable<Skill> GetAll();
    }
}
