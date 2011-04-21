using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Catechize.Model
{
    public class Part : Translatable<PartLanguage>
    {
        [Key]
        public int PartID { get; set; }

        public string CourseID { get; set; }
        public int OrdinalNo { get; set; }

        public virtual ICollection<Page> Pages { get; set; }
    }

    public class PartLanguage : HasCulture
    {
        [Key]
        public int PartLanguageID { get; set; }
        public int CourseID { get; set; }
        public int OrdinalNo { get; set; }
        public string Title { get; set; }
    }
}