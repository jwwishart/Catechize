using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class Course
    {
        public Guid CourseID {get; set;}
        public string Identifier { get; set; } // TODO: Note: CourseNameUrl
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
        public IList<int> PrerequisiteCourses { get; set; }
    }
}
