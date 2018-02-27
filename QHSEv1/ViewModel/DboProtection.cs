using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class DboProtection
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dbo_protection_programs_date { get; set; }
        public string project_name { get; set; }
        public string work_area_department { get; set; }
        public string description_of_program { get; set; }
        public string version_number { get; set; }
        public Nullable<System.DateTime> revision_date { get; set; }
        public Nullable<System.DateTime> next_revision_date { get; set; }
        public string notes { get; set; }
        public string attach_program { get; set; }
        public string attach_other { get; set; }
        public string dboProtectionDateStr { get; set; }
        public string userID { get; set; }
    }
}