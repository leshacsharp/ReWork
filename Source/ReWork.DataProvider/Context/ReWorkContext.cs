using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Entities;
using System.Data.Entity;

namespace ReWork.DataProvider.Context
{
    public class ReWorkContext : IdentityDbContext<User>
    {
        public ReWorkContext() : base("ReWorkConnectionString")
        {
            Database.SetInitializer(new ReWorkInitializer());
        }

        public ReWorkContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new ReWorkInitializer());
        }

        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
