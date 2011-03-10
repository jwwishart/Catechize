using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Catechize.Model
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Identifier { get; set; } // TODO: Note: CourseNameUrl
        public string Title { get; set; }
        public string Description { get; set; }

        // Gets or sets whether the course is accessble by students in any language
        // regardless of the state of each individual language.
        public bool IsEnabled { get; set; }

        public IList<int> PrerequisiteCourses { get; set; }
        public IList<CourseLanguage> LanguageVersions { get; set; }

        public CourseLanguage GetLanguage(CultureInfo cultureInfo)
        {
            foreach (var cLang in LanguageVersions)
            {
                if (cLang.Culture.Equals(cultureInfo))
                    return cLang;
            }

            return null;
        }

        public CourseLanguage GetLanguage(string cultureName) {
            return GetLanguage(new CultureInfo(cultureName));
        }
    }

    public class CourseLanguage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public CultureInfo Culture { get; set; }
        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
    }
}
