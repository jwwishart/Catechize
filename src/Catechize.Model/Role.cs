using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class Role
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }


    // TODO: Think about how best to implement this?
    class RoleNames
    {
        public static string Master = "master";
        public static string Administrator = "admin";
        public static string Translator = "translator";
        public static string Student = "student";
        public static string Designer = "designer";
        public static string Grader = "grader";
        public static string Manager = "manager"; 
    }
}
