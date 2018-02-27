using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class ToolBox
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string toolBoxDateStr { get; set; }
        public string topic_desc { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string topic { get; set; }
        public string question_concern { get; set; }
        public string developer { get; set; }
        public string supervisor { get; set; }
        public string attach_tool { get; set; }
        public string other_attachment { get; set; }
        public string userID { get; set; }
    }
}