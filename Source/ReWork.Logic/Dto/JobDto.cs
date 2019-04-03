using System;
using System.Collections.Generic;

namespace ReWork.Logic.Dto
{
    public class JobDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
      
        public string Description { get; set; }

        public int? Price { get; set; }

        public DateTime DateAdded { get; set; }


        public CustomerProfileDto Customer { get; set; }

        public EmployeeProfileDto Employee { get; set; }



        public ICollection<OfferDto> Offers { get; set; }

        public FeedBackDto FeedBack { get; set; }

        public ICollection<SkillDto> Skills { get; set; }
    }
}
