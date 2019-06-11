using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Implementation
{
    public class SectionService : ISectionService
    {
        private ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public IEnumerable<SectionInfo> GetSectionsInfo()
        {
            return _sectionRepository.GetSectionsInfo();
        }

        public IEnumerable<Section> GetAll()
        {
            return _sectionRepository.GetAll();
        }
    }
}
