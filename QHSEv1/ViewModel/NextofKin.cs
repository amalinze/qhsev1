using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class NextofKin
    {
        public int id { get; set; }
        public string name { get; set; }
        public string kin_full_name { get; set; }
        public string gender { get; set; }
        public string relationship { get; set; }
        public string description { get; set; }
        public string home_phone { get; set; }
        public string cell_phone { get; set; }
        public string email_address { get; set; }
        public string address_option { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string state_province { get; set; }
        public string country { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string nextofKinDateStr { get; set; }
        public string userID { get; set; }
    }
}