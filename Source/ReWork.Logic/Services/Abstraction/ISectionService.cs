using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ISectionService
    {
        IEnumerable<SectionInfo> GetSectionsInfo();
        IEnumerable<Section> GetAll(); 
    }
}
