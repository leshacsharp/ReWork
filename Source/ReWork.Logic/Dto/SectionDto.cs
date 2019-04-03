using System.Collections.Generic;

namespace ReWork.Logic.Dto
{
    public class SectionDto
    {  
        public string Title { get; set; }

        public ICollection<SkillDto> Skills { get; set; }
    }
}
