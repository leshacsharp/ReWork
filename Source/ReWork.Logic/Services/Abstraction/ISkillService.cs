using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ISkillService
    {
        Skill FindById(int id);
        IEnumerable<Skill> GetAll();
    }
}
