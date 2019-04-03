using ReWork.DataProvider.Entities;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ISectionRepository : IRepository<Section>
    {
        Section FindSectionByTitle(string title);
    }
}
