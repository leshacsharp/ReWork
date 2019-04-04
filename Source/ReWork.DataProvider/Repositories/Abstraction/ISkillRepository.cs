using ReWork.Model.Entities;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Skill FindSkillByTitle(string title);
    }
}
