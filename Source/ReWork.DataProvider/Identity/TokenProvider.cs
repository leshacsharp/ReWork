using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using ReWork.Model.Entities;

namespace ReWork.DataProvider.Identity
{
    public static class TokenProvider
    {
        private static DataProtectorTokenProvider<User> _tokenProvider;

        public static DataProtectorTokenProvider<User> Provider
        {
            get
            {
                if (_tokenProvider != null)
                    return _tokenProvider;

                //DpapiDataProtectionProvider dataProtectionProvider = new DpapiDataProtectionProvider();
                //var dataProtectionProvider = 
                //_tokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create());
                // return _tokenProvider;
                return null;
            }
        }
    }
}
