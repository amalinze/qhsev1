using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class CorRemAction
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string action_type { get; set; }
        public string action_name { get; set; }
        public string action_rem_by { get; set; }
        public string action_rem { get; set; }
        public string action_implimented { get; set; }
        public Nullable<System.DateTime> completion_date { get; set; }
        public string action_com_by { get; set; }
        public string results_of_actions { get; set; }
        public string staatus_of_action { get; set; }
        public string attachment { get; set; }
        public string corRemActionDateStr { get; set; }
        public string userID { get; set; }
    }
}