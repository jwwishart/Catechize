using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Services
{
    public interface ITest
    {
        string GetString();
    }

    public class Test : ITest
    {
        public string GetString()
        {
            return "WOW THIS WORKS!";
        }
    }
}
