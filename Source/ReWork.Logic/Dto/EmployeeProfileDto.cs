using System.Collections.Generic;

namespace ReWork.Logic.Dto
{
    public class EmployeeProfileDto
    {
        public string Id { get; set; }

        public int Age { get; set; }

        public string UserName { get; set; }

        public IEnumerable<SkillDto> Skills { get; set; }
    }
}
