using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Catechize.Customization
{
    class CustomIdentity : IIdentity
    {
        private string _username;

        public CustomIdentity(string username, string cultureName)
        {
            this._username = username;
            this.CultureName = cultureName;
        }

        public string CultureName { get; private set; }

        public string Name
        {
            get { return this._username; }
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}