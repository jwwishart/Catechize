using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
