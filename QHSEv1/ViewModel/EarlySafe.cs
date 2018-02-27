using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class EarlySafe
    {

        public int id { get; set; }
        public string plan_type { get; set; }
        public string incident_ref_no { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string earlySafeDateStr { get; set; }
        public string name { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string position { get; set; }
        public string claim_no { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> ancipated_date { get; set; }
        public string worker_phone_no { get; set; }
        public string worker_email_addr { get; set; }
        public string progress_report_req { get; set; }
        public string injury_details { get; set; }
        public string supervisor { get; set; }
        public string goals { get; set; }
        public string objective { get; set; }
        public string plan_dev_coop { get; set; }
        public string worker_restrictions { get; set; }
        public string details_on_worker { get; set; }
        public string duties { get; set; }
        public string hours { get; set; }
        public string worker { get; set; }
        public string manager { get; set; }
        public string return_to_work { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string attachment_3 { get; set; }
        public string userID { get; set; }
    }
}