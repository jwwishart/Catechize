using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Catechize.Helpers
{
    public static class IPrincipalExtensions
    {
        public static bool CanManageCourses(this IPrincipal user)
        {
            return user.IsInRole("Master") ||
                user.IsInRole("Administrator") ||
                user.IsInRole("Manager");
        }

        public static bool IsStudent(this IPrincipal user)
        {
            if (user.Identity == null)
                return false;

            if (user.Identity.IsAuthenticated == false)
                return false;

            if (user.IsInRole("student"))
                return true;

            return false;
        }

        public static bool IsAnonymous(this IPrincipal user)
        {
            if (user.Identity == null)
                return true;

            if (user.Identity.IsAuthenticated == false)
                return true;

            return false;
        }


    }
}