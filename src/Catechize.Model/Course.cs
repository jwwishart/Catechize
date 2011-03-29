using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Catechize.Model
{
    public class Course : Translatable<CourseLanguage>
    {
        public int CourseID { get; set; }

        // Max Length: 50 characters
        public string Identifier { get; set; } // TODO: Note: CourseNameUrl
        // Max Length: 
        public string Title { get; set; }
        public string Description { get; set; }

        // Gets or sets whether the course is accessble by students in any language
        // regardless of the state of each individual language.
        public bool IsEnabled { get; set; }

        public IList<int> PrerequisiteCourses { get; set; }
    }

    public class CourseLanguage : IHasCulture
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public CultureInfo Culture { get; set; }
        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
    }
}
