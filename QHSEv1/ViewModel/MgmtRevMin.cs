using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class MgmtRevMin
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dt_of_meeting { get; set; }
        public string meeting_title { get; set; }
        public string obj_discussed { get; set; }
        public string meeting_cllaed_by { get; set; }
        public string min_sub_by { get; set; }
        public string loc_of_meeting { get; set; }
        public string project_name { get; set; }
        public string topics_discussed { get; set; }
        public string desc_if_other { get; set; }
        public string in_attendance { get; set; }
        public string meeting_top_iss { get; set; }
        public string add_comm_ins { get; set; }
        public string sign_in_sheet { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string attachment_4 { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string mgmtRevMinDateStr { get; set; }
        public string userID { get; set; }
    }
}