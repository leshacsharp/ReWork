using ReWork.Model.Entities.Common;
using System;
using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class CustomerProfileInfo
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Image { get; set; }

        public DateTime RegistrationdDate { get; set; }

        public int CountPublishJobs { get; set; }

        public IEnumerable<QualityOfWork> QualityOfWorks { get; set; }
    }
}
