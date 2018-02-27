using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class HarzardNm
    {
        public int id { get; set; }
        public string incident_ref { get; set; }
        public string event_status { get; set; }
        public string event_type { get; set; }
        public Nullable<System.DateTime> date_of_event { get; set; }
        public string hazardNmDateStr { get; set; }
        public Nullable<System.DateTime> report_date { get; set; }
        public string project_name { get; set; }
        public string brief_desc { get; set; }
        public string detailed_desc { get; set; }
        public string observe_comments { get; set; }
        public string recommended_cor_actions { get; set; }
        public string immediate_cor_actions { get; set; }
        public string violation_of_rules { get; set; }
        public string motor_veh_inc { get; set; }
        public string immediate_action { get; set; }
        public string location { get; set; }
        public string exposure_freequency { get; set; }
        public string eyewitness_statement { get; set; }
        public string risk_of_injury { get; set; }
        public string probability { get; set; }
        public string investigation_attached { get; set; }
        public string investigation_report { get; set; }
        public string other_attachment { get; set; }
        public string userID { get; set; }
    }
}