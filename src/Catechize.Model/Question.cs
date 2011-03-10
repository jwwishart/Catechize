using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Model
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int CourseID { get; set; }
        public int PartID { get; set; }
        public int PageID { get; set; }
        public int AnswerID { get; set; }
    }
}
