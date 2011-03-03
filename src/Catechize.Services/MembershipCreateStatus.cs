using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Services
{
    public enum MembershipCreateStatus
    {
        Success = 0,
        InvalidUserName = 1,
        InvalidPassword = 2,
        InvalidEmail = 5,
        DuplicateUserName = 6,
    }
}
