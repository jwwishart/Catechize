using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class Part
    {
        public int PartID {get; set;}
        public int CourseID { get; set; }
        public int OrdinalNo { get; set; }
    }

    public class PartLanguage
    {
        public int CourseID { get; set; }
        public int OrdinalNo { get; set; }
        public string Title { get; set; }
    }
}
