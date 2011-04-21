using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Catechize.Model
{
    public class CourseLanguage : HasCulture
    {
        [Key]
        public int CourseLanguageID { get; set; }
        public string CourseID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
    }
}
