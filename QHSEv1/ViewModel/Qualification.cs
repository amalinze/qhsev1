using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class Qualification
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string qualificationDateStr { get; set; }
        public string name { get; set; }
        public string work_department { get; set; }
        public string position { get; set; }
        public Nullable<System.DateTime> qualification_date { get; set; }
        public Nullable<System.DateTime> q_expiry_date { get; set; }
        public string qualification1 { get; set; }
        public string notes { get; set; }
        public string attach_certificate { get; set; }
        public string userID { get; set; }
    }
}