//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QHSEv1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class company_information
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
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