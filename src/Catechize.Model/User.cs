using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Catechize.Model
{
    public class User
    {
        #region Attributes
        [Key]
        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(50, MinimumLength=4)]
        #endregion
        public string Username { get; set; }

        #region
        [Required(ErrorMessageResourceName = "FieldRequired",
                  ErrorMessageResourceType = typeof(Catechize.Model.Resources.Validation))]
        [StringLength(254, MinimumLength = 3)]
        #endregion
        public string Email { get; set; }

        public IList<string> Roles { get; set; }
    }
}
