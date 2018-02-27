using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class Orientation
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string orientationDateStr { get; set; }
        public Nullable<System.DateTime> review_date { get; set; }
        public string work_dept { get; set; }
        public string desc_train_ori { get; set; }
        public string notes { get; set; }
        public string orient_file_1 { get; set; }
        public string orient_fiile_2 { get; set; }
        public string orient_file_3 { get; set; }
        public string orient_file_4 { get; set; }
        public string userID { get; set; }
    }
}