using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class DboAudits
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public Nullable<System.DateTime> dbo_audit_date { get; set; }
        public string type_of_audit { get; set; }
        public string project_name { get; set; }
        public string description { get; set; }
        public string completed_by { get; set; }
        public string auditing_organization { get; set; }
        public string score { get; set; }
        public string notes { get; set; }
        public string attach_audits { get; set; }
        public string attach_other { get; set; }
        public string dboAuditsDateStr { get; set; }
        public string userID { get; set; }
    }
}