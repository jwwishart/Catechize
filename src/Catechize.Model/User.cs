using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace Catechize.Model
{
    public class UserProfile
    {
        #region Attributes
        [Key]
        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(50, MinimumLength=4)]
        #endregion
        public string Username { get; set; }

        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}
