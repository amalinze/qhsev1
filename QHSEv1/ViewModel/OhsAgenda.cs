using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class OhsAgenda
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dt_meeting { get; set; }
        public string ohsAgendaDateStr { get; set; }
        public string project_name { get; set; }
        public string total_no_emp { get; set; }
        public string no_issues_def { get; set; }
        public string required_attendance { get; set; }
        public string employer_co_chair { get; set; }
        public string worker_co_chair { get; set; }
        public string guests { get; set; }
        public string no_of_workplace_con { get; set; }
        public string no_issues_identified { get; set; }
        public string no_complaint_received { get; set; }
        public string were_there_issues { get; set; }
        public string no_acc_inv_con { get; set; }
        public string no_inc_inv_con { get; set; }
        public string were_there_any_issues { get; set; }
        public string no_inc_report_review { get; set; }
        public string no_acc_report_review { get; set; }
        public string no_work_refusals { get; set; }
        public string meeting_topics_issues { get; set; }
        public string add_com_ins { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string userID { get; set; }
    }
}