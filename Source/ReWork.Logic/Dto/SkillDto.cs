using System.Collections.Generic;

namespace ReWork.Logic.Dto
{
    public class SkillDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
   
        public SectionDto Section { get; set; }

        public ICollection<EmployeeProfileDto> Employes { get; set; }

        public ICollection<JobDto> Jobs { get; set; }
    }
}
