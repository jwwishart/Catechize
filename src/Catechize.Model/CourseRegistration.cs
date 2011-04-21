using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Catechize.Model
{
    public class CourseRegistration
    {
        [Key]
        public int CourseRegistrationID { get; set; }

        public string CourseID { get; set; }
        public int LanguageID { get; set; }
        public string UserID { get; set; }
    }
}
