using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catechize.Model;

namespace Catechize.Models
{
    public class PersonalUserProfileModel : PrivateUserProfileModel
    {
        public ICollection<Course> AvailableCourses { get; set; }
        public ICollection<CourseRegistration> RegisteredCourses { get; set; }
    }

    public class PrivateUserProfileModel
    {
        public string Username { get; set; }
        public string Email { get; set; } // For Gravatar
    }


}