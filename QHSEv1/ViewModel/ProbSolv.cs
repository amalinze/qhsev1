using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class ProbSolv
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string probSolvDateStr { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string meeting_leader { get; set; }
        public string problem { get; set; }
        public string analysis { get; set; }
        public string corrective_action { get; set; }
        public string follow_up { get; set; }
        public string other_PAA { get; set; }
        public string attendance { get; set; }
        public string reviewed_by { get; set; }
        public string attachment_1 { get; set; }
        public string attchment_2 { get; set; }
        public string userID { get; set; }
    }
}