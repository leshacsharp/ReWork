using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Entities;

namespace ReWork.DataProvider.Identity
{
    public class AppUserManager : UserManager<User>    
    {
        public AppUserManager(IUserStore<User> store)
            :base(store)
        {

        }
    }
}
