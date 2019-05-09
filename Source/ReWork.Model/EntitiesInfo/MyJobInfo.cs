using System;
using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class MyJobInfo
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }

        public DateTime DateAdded { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeUserName { get; set; }

        public IEnumerable<SkillInfo> Skills { get; set; }
    }
}
