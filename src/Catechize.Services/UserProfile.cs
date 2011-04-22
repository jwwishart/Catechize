using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Globalization;

namespace Catechize.Services
{
    public class UserProfile : ProfileBase
    {
        public string FirstName { get; set; }
        public string Initial { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }

        public CultureInfo DefaultCulture { get; set; }
    }
}
