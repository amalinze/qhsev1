using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class MeetingPlan
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string meetingPlanDateStr { get; set; }
        public string dt_meeting { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string meeting_subject { get; set; }
        public string meeting_description { get; set; }
        public string required_attendance { get; set; }
        public string gen_obj_meting { get; set; }
        public string desired_end_result { get; set; }
        public string planning_detail { get; set; }
        public string meeting_prep_checklist { get; set; }
        public string attacchment_1 { get; set; }
        public string attacment { get; set; }
        public string userID { get; set; }
    }
}