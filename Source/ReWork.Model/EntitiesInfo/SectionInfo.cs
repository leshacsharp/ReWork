using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class SectionInfo
    {
        public string Title { get; set; }

        public IEnumerable<SkillSectionInfo> Skills { get; set; }
    }
}
