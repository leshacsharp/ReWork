using ReWork.Model.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ReWork.Model.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }
        DbSet<CustomerProfile> CustomerProfiles { get; set; }
        DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        DbSet<Job> Jobs { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<FeedBack> FeedBacks { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Skill> Skills { get; set; }


        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}
