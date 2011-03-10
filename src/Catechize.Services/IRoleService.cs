﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IRoleService
    {
        IList<Role> GetAll();
        Role GetRole(string roleName);
    }
}
