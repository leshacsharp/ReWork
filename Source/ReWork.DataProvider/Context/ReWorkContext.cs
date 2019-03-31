using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace ReWork.DataProvider.Context
{
    public class ReWorkContext : IdentityDbContext<User>
    {
        public ReWorkContext() : base("ReWorkConnectionString")
        { 
        }

        public ReWorkContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
