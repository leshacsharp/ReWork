using System.Collections.Generic;

namespace ReWork.Logic.Dto
{
    public class CustomerProfileDto
    {
        public string UserName { get; set; }

        public ICollection<JobDto> Jobs { get; set; }
    }
}
