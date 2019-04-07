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
    public class SectionService : ISectionService
    {
        private ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRep)
        {
            _sectionRepository = sectionRep;
        }

        public IEnumerable<Section> GetAll()
        {
            return _sectionRepository.GetAll();
        }
    }
}
