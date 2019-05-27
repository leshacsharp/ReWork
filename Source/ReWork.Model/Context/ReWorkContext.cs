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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<FeedBack>(u => u.RecivedFeedBacks)
                .WithRequired(f => f.Receiver)
                .HasForeignKey<string>(f => f.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
             .HasMany<FeedBack>(u => u.SentFeedBacks)
             .WithRequired(f => f.Sender)
             .HasForeignKey<string>(f => f.SenderId)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
              .HasMany<Notification>(u => u.RecivedNotifications)
              .WithRequired(n => n.Reciver)
              .HasForeignKey<string>(n => n.ReciverId);

            modelBuilder.Entity<User>()
             .HasMany<Notification>(u => u.SentNofications)
             .WithRequired(n => n.Sender)
             .HasForeignKey<string>(n => n.SenderId)
             .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
