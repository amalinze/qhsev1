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
    
    public partial class emerg_rescue_plan
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string plan_desc { get; set; }
        public Nullable<System.DateTime> dev_date { get; set; }
        public Nullable<System.DateTime> next_rev_date { get; set; }
        public string notes { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string attachment_4 { get; set; }
        public string userID { get; set; }
    }
}