using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISectionRepository : IRepository<Section>
    {
        Section FindSectionByTitle(string title);
        IEnumerable<Section> GetAll();
    }
}
