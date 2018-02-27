using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class DisciplinaryAction
    {

        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string disciplinaryActionDateStr { get; set; }
        public string employee_name { get; set; }
        public string project_name { get; set; }
        public string work_area { get; set; }
        public string position { get; set; }
        public string company { get; set; }
        public string supervisor_name { get; set; }
        public string action_taken { get; set; }
        public string offences { get; set; }
        public string dbo_dept_notified { get; set; }
        public string appr_manager_notified { get; set; }
        public string employee_comments { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string userID { get; set; }
    }
}