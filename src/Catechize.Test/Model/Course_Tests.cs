using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Catechize.Model;
using System.Globalization;

namespace Catechize.Test.Model
{
    public class Course_Tests
    {
        [Fact]
        public void GetLanguage_PassCultureNotInList_ReturnsNull()
        {
            string validCultureID = "en";
            string cultureNotInCourse = "sq";

            var newCourse = new Course()
            {
                Translations = new List<CourseLanguage>() {
                    new CourseLanguage() {
                         Culture = new CultureInfo(validCultureID)
                    }
                }
            };

            Assert.Null(newCourse.GetLanguage(new CultureInfo(cultureNotInCourse)));
        }

        [Fact]
        public void GetLanguage_PassCultureThatIsInList_ReturnsCourseLanguage()
        {
            string validCultureID = "en";

            var newCourse = new Course()
            {
                Translations = new List<CourseLanguage>() {
                    new CourseLanguage() {
                         Culture = new CultureInfo(validCultureID)
                    }
                }
            };

            Assert.NotNull(newCourse.GetLanguage(new CultureInfo(validCultureID)));
        }

        [Fact]
        public void GetLanguage_PassCultureNameNotInList_ReturnsNull()
        {
            string validCultureID = "en";
            string cultureNotInCourse = "sq";

            var newCourse = new Course()
            {
                Translations = new List<CourseLanguage>() {
                    new CourseLanguage() {
                         Culture = new CultureInfo(validCultureID)
                    }
                }
            };

            Assert.Null(newCourse.GetLanguage(cultureNotInCourse));
        }

        [Fact]
        public void GetLanguage_PassCultureNameThatIsInList_ReturnsCourseLanguage()
        {
            string validCultureID = "en";

            var newCourse = new Course()
            {
                Translations = new List<CourseLanguage>() {
                    new CourseLanguage() {
                         Culture = new CultureInfo(validCultureID)
                    }
                }
            };

            Assert.NotNull(newCourse.GetLanguage(validCultureID));
        }

    }
}
