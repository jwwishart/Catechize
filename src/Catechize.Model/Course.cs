using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class Course
    {
        public int CourseID {get; set;}
        public string Title { get; set; }
        public string Url { get; set; } // TODO: NOTE: Specifies string used for the {course} part of the Route (MVC SPECIFIC)
    }
}
