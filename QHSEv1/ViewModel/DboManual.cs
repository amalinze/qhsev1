using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class DboManual
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dbo_manual_date { get; set; }
        public Nullable<System.DateTime> development_date { get; set; }
        public Nullable<System.DateTime> revision_date { get; set; }
        public Nullable<System.DateTime> next_revision_date { get; set; }
        public string version_number { get; set; }
        public string update_number { get; set; }
        public string revised_by { get; set; }
        public string approved_by { get; set; }
        public string attach_dbo_manual { get; set; }
        public string attach_other { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string dboManualDateStr { get; set; }
        public string userID { get; set; }
    }
}