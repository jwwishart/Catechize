using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Catechize.Model
{
    public class Course : Translatable<CourseLanguage>
    {
        public int CourseID { get; set; }

        [Required(ErrorMessageResourceName="FieldRequired", 
                  ErrorMessageResourceType=typeof(Catechize.Model.Resources.Validation)) ]
        [StringLength(50)]
        public string Identifier { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(200, ErrorMessageResourceName = "StringToLong",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(1000, ErrorMessageResourceName = "StringToLong",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Gets or sets whether the course is accessble by students in any language
        // regardless of the state of each individual language.
        public bool IsEnabled { get; set; }

        public virtual ICollection<int> PrerequisiteCourses { get; set; }
    }

    public class CourseLanguage : IHasCulture
    {
        public int CourseLanguageID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public CultureInfo Culture { get; set; }
        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
    }
}
