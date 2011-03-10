using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class Element
    {
        public int ElementID { get; set; }
        public int PageID { get; set; }
        public int Ordinal { get; set; }
        public CourseLanguage Language { get; set; }
    }
}
