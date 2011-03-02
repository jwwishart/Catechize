using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Catechize.Model
{
    public class Student
    {
        public int StudentID {get;set;}
        public string FirstName { get; set; }
        public string Initial { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public CultureInfo Culture { get; set; }

        public StudentType StudentType { get; set; } 
    }
}