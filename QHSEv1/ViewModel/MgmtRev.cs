using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class MgmtRev
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string mgmtRevDateStr { get; set; }
        public string meeting_time { get; set; }
        public string objective { get; set; }
        public string meeting_called_by { get; set; }
        public Nullable<System.DateTime> dt_of_meeting { get; set; }
        public string project_name { get; set; }
        public string location { get; set; }
        public string topics_of_discussion { get; set; }
        public string desc_if_other { get; set; }
        public string attendance_req_by { get; set; }
        public string spec_meeting_top { get; set; }
        public string add_com_ins { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string userID { get; set; }
    }
}