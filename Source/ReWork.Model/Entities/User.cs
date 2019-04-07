using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public DateTime RegistrationdDate { get; set; }

        public Status Status { get; set; }

        public virtual CustomerProfile CustomerProfile{ get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
    }
}
