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
    
    public partial class qualification
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
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