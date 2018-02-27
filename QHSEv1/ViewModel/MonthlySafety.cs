using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class MonthlySafety
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string monthlySafetyDateStr { get; set; }
        public string project_name { get; set; }
        public string for_month { get; set; }
        public string for_year { get; set; }
        public string inspections { get; set; }
        public string internal_audits { get; set; }
        public string mgmt_safety_visit { get; set; }
        public string emerg_res_drill { get; set; }
        public string safety_orientation { get; set; }
        public string JSA { get; set; }
        public string remedial_actions { get; set; }
        public string training_hours { get; set; }
        public string work_refusals { get; set; }
        public string tailgate_toolbox { get; set; }
        public string gen_safety_meeting { get; set; }
        public string dept_area { get; set; }
        public string mgmt_review { get; set; }
        public string fatalities { get; set; }
        public string lost_time_inc { get; set; }
        public string restricted_work_cases { get; set; }
        public string medical_treatment { get; set; }
        public string first_aid_treatment { get; set; }
        public string non_work_related { get; set; }
        public string motor_vehicle_inc { get; set; }
        public string near_misses { get; set; }
        public string equipment_damage { get; set; }
        public string equipment_losses { get; set; }
        public string environment_dist { get; set; }
        public string attachment { get; set; }
        public string userID { get; set; }
    }
}