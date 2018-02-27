using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class ReturnPlan
    {
        public int id { get; set; }
        public string incident_ref_no { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string returnPlanDateStr { get; set; }
        public string name { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string position { get; set; }
        public string claim_no { get; set; }
        public string supevisor { get; set; }
        public string RTW_coordinator { get; set; }
        public string week_no { get; set; }
        public string objectives { get; set; }
        public string limitations { get; set; }
        public string duties { get; set; }
        public string hours { get; set; }
        public string employee_comm_con { get; set; }
        public string action_to_addr { get; set; }
        public string userID { get; set; }
    }
}