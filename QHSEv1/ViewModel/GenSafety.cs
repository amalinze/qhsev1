using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class GenSafety
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dt_of_meeting { get; set; }
        public string genSafetyDateStr { get; set; }
        public string project_name { get; set; }
        public string work_area { get; set; }
        public string other_dept { get; set; }
        public string shift { get; set; }
        public string no_in_crew { get; set; }
        public string no_attending { get; set; }
        public string topic_disc { get; set; }
        public string employee_con_sug { get; set; }
        public string OHS_con_sug { get; set; }
        public string attended_by { get; set; }
        public string managers_remarks { get; set; }
        public string presenter { get; set; }
        public string supervisor { get; set; }
        public string manager { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string userID { get; set; }
    }
}