using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        public DateTime? RegistrationdDate { get; set; }

        public Status Status { get; set; }

        public byte[] Image { get; set; }

        public virtual CustomerProfile CustomerProfile{ get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
    }
}
