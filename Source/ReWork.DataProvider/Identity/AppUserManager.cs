using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.Model.Context;
using ReWork.Model.Entities;

namespace ReWork.DataProvider.Identity
{
    public class AppUserManager : UserManager<User>    
    {
        public AppUserManager(IDbContext context)
            :base(new UserStore<User>((ReWorkContext)context))
        {

        }
    }
}
