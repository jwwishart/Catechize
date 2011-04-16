using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Catechize.Model
{
    public class Part : Translatable<PartLanguage>
    {
        public int PartID {get; set;}
        public int CourseID { get; set; }
        public int OrdinalNo { get; set; }
    }

    public class PartLanguage : IHasCulture
    {
        public int CourseID { get; set; }
        public int OrdinalNo { get; set; }
        public string Title { get; set; }
        //public CultureInfo Culture { get; set; }
        public string Culture { get; set; }
    }
}
