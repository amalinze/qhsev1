using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class GeneralInfo
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string currently_employed { get; set; }
        public string position { get; set; }
        public string project_name { get; set; }
        public string work_area_dept { get; set; }
        public string gender { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string state_province { get; set; }
        public string postal_code { get; set; }
        public string home_number { get; set; }
        public string cell_number { get; set; }
        public string email_address { get; set; }
        public string medical_card_number { get; set; }
        public string drivers_license_class { get; set; }
        public string medical_condition { get; set; }
        public string insurance_coverage { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string generalInfoDateStr { get; set; }
        public string userID { get; set; }
    }
}