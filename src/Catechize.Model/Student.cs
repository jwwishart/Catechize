﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Catechize.Model
{
    public class Student
    {
        public int StudentID {get;set;}
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string Initial { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public CultureInfo DefaultCulture { get; set; }
        public string Country { get; set; } // TODO: How do we store this in an adequate way???
        
        public StudentType StudentType { get; set; } 
    }
}