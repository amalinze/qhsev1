using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class WitnessStmt
    {
        public int id { get; set; }
        public string incident_ref_no { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string name { get; set; }
        public string project_name { get; set; }
        public string work_area_dept { get; set; }
        public string position { get; set; }
        public string name_TP { get; set; }
        public string phone { get; set; }
        public string email_addr { get; set; }
        public string addrress { get; set; }
        public string state_province { get; set; }
        public string city { get; set; }
        public string employer { get; set; }
        public string first_wit_name { get; set; }
        public string first_wit_phone { get; set; }
        public string first_wit_comp { get; set; }
        public string second_wit_name { get; set; }
        public string second_wit_phone { get; set; }
        public string second_wit_comp { get; set; }
        public string third_wit_name { get; set; }
        public string third_wit_phone { get; set; }
        public string third_wit_comp { get; set; }
        public string incident_desc { get; set; }
        public string statement { get; set; }
        public string signature { get; set; }
        public Nullable<System.DateTime> sig_date { get; set; }
        public string signed_witness_stmt { get; set; }
        public string attachment_1 { get; set; }
        public string attachment_2 { get; set; }
        public string witnessStmtDateStr { get; set; }
        public string userID { get; set; }
    }
}