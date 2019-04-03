using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReWork.DataProvider.Identity
{
    public class AppRoleManager : RoleManager<IdentityRole>
    {
        public AppRoleManager(RoleStore<IdentityRole> store)
            : base(store)
        {

        }
    }
}
