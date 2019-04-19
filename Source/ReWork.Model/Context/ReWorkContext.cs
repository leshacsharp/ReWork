using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.Model.Entities;
using ReWork.Model.Migrations;
using System.Data.Entity;


namespace ReWork.Model.Context
{
    public class ReWorkContext : IdentityDbContext<User>, IDbContext
    {
        public ReWorkContext() : base("ReWorkConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReWorkContext, Configuration>());
        }

        public ReWorkContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReWorkContext, Configuration>());
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
