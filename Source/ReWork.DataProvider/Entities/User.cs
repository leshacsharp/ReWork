using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReWork.DataProvider.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public DateTime RegistrationdDate { get; set; }

        public Status Status { get; set; }

        public virtual CustomerProfile CustomerProfile{ get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
    }
}
