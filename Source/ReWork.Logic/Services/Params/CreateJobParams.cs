using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Params
{
    public class CreateJobParams
    {
        public string CustomerUserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }
       

        public IEnumerable<int> SkillsId { get; set; }
    }
}
