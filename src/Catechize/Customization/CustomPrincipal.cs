using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

namespace Catechize.Customization
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity _identity = null;

        public CustomPrincipal(CustomIdentity identity)
        {
            this._identity = identity;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(this.Identity.Name, role);
        }
    }
}