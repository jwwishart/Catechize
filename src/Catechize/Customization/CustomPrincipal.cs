using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Catechize.Customization
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity _identity = null;
        private string[] _roles = null;

        public CustomPrincipal(CustomIdentity identity, string[] roles)
        {
            this._identity = identity;
            this._roles = roles;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            foreach (string r in _roles)
            {
                if (String.Equals(r, role, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}