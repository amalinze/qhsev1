using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class SafeProc
    {
        public int id { get; set; }
        public string safe_work_procedure_description { get; set; }
        public Nullable<System.DateTime> development_date { get; set; }
        public string version_number { get; set; }
        public Nullable<System.DateTime> review_date { get; set; }
        public Nullable<System.DateTime> next_review_date { get; set; }
        public string swp_notes { get; set; }
        public string attach_other { get; set; }
        public string attach_swp { get; set; }
        public string safeProcDateStr { get; set; }
        public string userID { get; set; }
    }
}