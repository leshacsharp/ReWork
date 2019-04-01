using System.Collections.Generic;

namespace ReWork.Logic.Dto
{
    public class CustomerProfileDto
    {
        public virtual UserDto User { get; set; }

        public virtual ICollection<JobDto> Jobs { get; set; }
    }
}
