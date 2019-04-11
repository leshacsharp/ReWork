using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Implementation
{
    public class SkillService : ISkillService
    {
        private ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public Skill FindById(int id)
        {
            return _skillRepository.FindById(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            return _skillRepository.GetAll();
        }
    }
}
