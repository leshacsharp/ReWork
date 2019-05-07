using ReWork.Model.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Helpers
{
    public static class UserProfle
    {
        public static string GetCurrentProfileName(this HtmlHelper helper)
        {
            HttpCookie profileCookie = HttpContext.Current.Request.Cookies["profile"];

            if (profileCookie != null)
                return profileCookie.Value;

            return null;
        }

        public static bool IsProfile(this HtmlHelper helper, ProfileType checkProfile)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpCookie profileCookie = HttpContext.Current.Request.Cookies["profile"];

                if (profileCookie != null)
                {
                    ProfileType profileType = (ProfileType)Enum.Parse(typeof(ProfileType), profileCookie.Value);
                    return profileType == checkProfile;
                }
            }

            return false;
        }
    }
}