using ReWork.Model.Entities.Common;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Employee
{
    public class EmployeeInfoViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public DateTime RegistrationdDate { get; set; }

        public IEnumerable<QualityOfWork> QualityOfWorks { get; set; }

        public IEnumerable<SkillInfo> Skills { get; set; }
    }
}
