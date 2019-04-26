using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISectionRepository : IRepository<Section>
    {
        Section FindSectionByTitle(string title);
        IEnumerable<SectionInfo> GetSectionsInfo();
        IEnumerable<Section> GetAll();
    }
}
