using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System;

namespace ReWork.DataProvider.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerProfileRepository CustomerProfileRepository { get; }
        IEmployeeProfileRepository EmployeeProfileRepository { get; }
        IJobRepository JobRepository { get; }
        IOfferRepository OfferRepository { get; }
        IFeedBackRepository FeedBackRepository { get; }
        ISectionRepository SectionRepository { get; }
        ISkillRepository SkillRepository { get; }

        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        void SaveChanges();
    }
}
