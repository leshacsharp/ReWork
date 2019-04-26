using ReWork.Model.Entities.Common;
using System;
using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class EmployeeProfileInfo
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Rating { get; set; }

        public int Age { get; set; }

        public DateTime RegistrationdDate { get; set; }

        public int CountPerfomedJobs { get; set; }

        public IEnumerable<SkillInfo> Skills{ get; set; }
    }
}
