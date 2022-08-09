using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public string QuestionContain { get; set; }
        public string Answer { get; set; }
    }
}
