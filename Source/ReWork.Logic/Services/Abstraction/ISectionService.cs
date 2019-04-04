using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ISectionService
    {
        IEnumerable<Section> GetSections();
    }
}
