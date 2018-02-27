using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class OhsComm
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dt_meeting { get; set; }
        public string ohsCommDateStr { get; set; }
        public string meeting_title { get; set; }
        public string meeting_called_by { get; set; }
        public string min_sub_by { get; set; }
        public string location_of_meet { get; set; }
        public string project_name { get; set; }
        public string employer_co_chair { get; set; }
        public string worker_co_chair { get; set; }
        public string comm_rep_cer { get; set; }
        public string meeting_topic { get; set; }
        public string add_comm_ins { get; set; }
        public string sign_in_sheet { get; set; }
        public string attachment_1 { get; set; }
        public string atachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string attachment_4 { get; set; }
        public string userID { get; set; }
    }
}