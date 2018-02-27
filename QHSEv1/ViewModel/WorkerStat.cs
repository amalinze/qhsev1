using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class WorkerStat
    {
        public int id { get; set; }
        public string worker_comp_insurace_reports_stats_date { get; set; }
        public string description { get; set; }
        public string project_name { get; set; }
        public string department_work_area { get; set; }
        public string details { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string workerStatDateStr { get; set; }
        public string userID { get; set; }
    }
}