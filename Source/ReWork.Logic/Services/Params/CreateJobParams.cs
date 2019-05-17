using System.Collections.Generic;

namespace ReWork.Logic.Services.Params
{
    public class CreateJobParams
    {
        public string CustomerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }
       

        public IEnumerable<int> SkillsId { get; set; }
    }
}
