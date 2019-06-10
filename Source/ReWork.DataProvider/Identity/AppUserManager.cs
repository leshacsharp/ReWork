using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using ReWork.Model.Context;
using ReWork.Model.Entities;

namespace ReWork.DataProvider.Identity
{
    public class AppUserManager : UserManager<User>    
    {
        public AppUserManager(IDbContext context, IDataProtectionProvider dataProtectionProvider)
            :base(new UserStore<User>((ReWorkContext)context))
        {
            this.EmailService = new EmailService();
            this.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create());
        }
    }
}
