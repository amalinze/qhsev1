using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class Audit
    {
        public int audit_guildlines_id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string auditDateStr { get; set; }
        public string description { get; set; }
        public string notes { get; set; }
        public string attach_audit_guildline { get; set; }
        public string attach_other { get; set; }
        public string userID { get; set; }

    }
}
