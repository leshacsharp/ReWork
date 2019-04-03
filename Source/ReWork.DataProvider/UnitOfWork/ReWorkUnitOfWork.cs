using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.DataProvider.Repositories.Implementation;

namespace ReWork.DataProvider.UnitOfWork
{
    public class ReWorkUnitOfWork : IUnitOfWork
    {
        private ReWorkContext _db;

        private ICustomerProfileRepository _customerProfileRepository;
        private IEmployeeProfileRepository _employeeProfileRepository;
        private IJobRepository _jobRepository;
        private IOfferRepository _offerRepository;
        private IFeedBackRepository _feedBackRepository;
        private ISectionRepository _sectionRepository;
        private ISkillRepository _skillRepository;

        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ReWorkUnitOfWork()
        {
            _db = new ReWorkContext();

            _userManager = new AppUserManager(new UserStore<User>(_db));
            _roleManager = new AppRoleManager(new RoleStore<IdentityRole>(_db));
        }

        public UserManager<User> UserManager    
        {
            get
            {
                return _userManager;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager;
            }
        }

        public ICustomerProfileRepository CustomerProfileRepository
        {
            get
            {
                if (_customerProfileRepository == null)
                    _customerProfileRepository = new CustomerProfilesRepository(_db);
                return _customerProfileRepository;

            }
        }

        public IEmployeeProfileRepository EmployeeProfileRepository
        {
            get
            {
                if (_employeeProfileRepository == null)
                    _employeeProfileRepository = new EmployeeProfileRepository(_db);
                return _employeeProfileRepository;

            }
        }

        public IJobRepository JobRepository
        {
            get
            {
                if (_jobRepository == null)
                    _jobRepository = new JobRepository(_db);
                return _jobRepository;

            }
        }

        public IOfferRepository OfferRepository
        {
            get
            {
                if (_offerRepository == null)
                    _offerRepository = new OfferRepository(_db);
                return _offerRepository;

            }
        }

        public IFeedBackRepository FeedBackRepository
        {
            get
            {
                if (_feedBackRepository == null)
                    _feedBackRepository = new FeedBackRepository(_db);
                return _feedBackRepository;

            }
        }

        public ISectionRepository SectionRepository
        {
            get
            {
                if (_sectionRepository == null)
                    _sectionRepository = new SectionRepository(_db);
                return _sectionRepository;

            }
        }

        public ISkillRepository SkillRepository
        {
            get
            {
                if (_skillRepository == null)
                    _skillRepository = new SkillRepository(_db);
                return _skillRepository;
            }
        }
     
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
