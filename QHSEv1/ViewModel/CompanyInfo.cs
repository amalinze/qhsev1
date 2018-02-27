using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class CompanyInfo
    {

        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string companyInfoDateStr { get; set; }
        public string company_name { get; set; }
        public string legal_name { get; set; }
        public string phone_number { get; set; }
        public string alternate_phone_number { get; set; }
        public string emergency_contact { get; set; }
        public string emergency_number { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string street_address { get; set; }
        public string state_province { get; set; }
        public string postal_code { get; set; }
        public string userID { get; set; }
    }
}