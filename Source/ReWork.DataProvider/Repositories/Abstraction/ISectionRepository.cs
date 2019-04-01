using ReWork.DataProvider.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISectionRepository : IRepository<Section>
    {
        IEnumerable<Section> FindSectionByTitle(string title);
    }
}
