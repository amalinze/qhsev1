using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class TrainAware
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string name { get; set; }
        public string project_name { get; set; }
        public string work_area { get; set; }
        public string position { get; set; }
        public string train_obtain { get; set; }
        public string train_desc { get; set; }
        public string train_prov { get; set; }
        public string certificat_number { get; set; }
        public Nullable<System.DateTime> issue_date { get; set; }
        public Nullable<System.DateTime> expiry_date { get; set; }
        public string cert_on_file { get; set; }
        public string notes { get; set; }
        public string certificate { get; set; }
        public string attachment_1 { get; set; }
        public string trainAwareDateStr { get; set; }
        public string userID { get; set; }
    }
}