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
        #region Attributes
        [Key]
        [Required(ErrorMessageResourceName="FieldRequired", 
                  ErrorMessageResourceType=typeof(Catechize.Model.Resources.Validation)) ]
        [StringLength(50)]
        #endregion
        public string Identifier { get; set; }

        #region Attributes
        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(200, ErrorMessageResourceName = "StringToLong",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        #endregion
        public string Title { get; set; }

        #region Attributes
        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(1000, ErrorMessageResourceName = "StringToLong",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [DataType(DataType.MultilineText)]
        #endregion
        public string Description { get; set; }

        // Gets or sets whether the course is accessble by students in any language
        // regardless of the state of each individual language.
        public bool IsEnabled { get; set; }

        public virtual ICollection<int> PrerequisiteCourses { get; set; }
    }

    public class CourseLanguage : HasCulture
    {
        public int CourseLanguageID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsPublished { get; set; }
        public bool IsEnabled { get; set; }
    }
}
