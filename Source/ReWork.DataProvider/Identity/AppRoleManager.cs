using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.Model.Context;

namespace ReWork.DataProvider.Identity
{
    public class AppRoleManager : RoleManager<IdentityRole>
    {
        public AppRoleManager(IDbContext context)
            : base(new RoleStore<IdentityRole>((ReWorkContext)context))
        {

        }
    }
}
