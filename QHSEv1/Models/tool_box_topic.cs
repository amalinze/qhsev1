//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QHSEv1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tool_box_topic
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
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
