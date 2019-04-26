using System;
using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class JobInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }

        public DateTime DateAdded { get; set; }

        public int CountOffers { get; set; }

        public string CustomerId { get; set; }

        public string UserName { get; set; }


        public IEnumerable<SkillInfo> Skills { get; set; }
    }
}
