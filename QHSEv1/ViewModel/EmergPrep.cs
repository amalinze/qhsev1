using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class EmergPrep
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string emergPrepDateStr { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string location { get; set; }
        public Nullable<System.DateTime> dt_of_practice { get; set; }
        public string description { get; set; }
        public string response_time { get; set; }
        public string evaluation_notes { get; set; }
        public string corr_action_recomm { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string attachment_4 { get; set; }
        public string userID { get; set; }
    }
}