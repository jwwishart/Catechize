using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{ 
    [Flags]
    public enum Role
    {
        None,
        Master,
        Administrator,
        Translator,
        Student,
        Designer,
        Grader,
        Manager
    }
}
