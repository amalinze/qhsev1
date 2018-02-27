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
    
    public partial class journey_mgmt
    {
        public int id { get; set; }
        public string driver_name { get; set; }
        public string co_driver_name { get; set; }
        public string project_name { get; set; }
        public string journey_status { get; set; }
        public string vehicle_id { get; set; }
        public string start_location { get; set; }
        public string destination { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public Nullable<System.DateTime> start_dt { get; set; }
        public Nullable<System.DateTime> arrival_dt { get; set; }
        public string total_distance { get; set; }
        public string total_travel_time { get; set; }
        public string rest_time { get; set; }
        public string supervisor_start { get; set; }
        public string supervisor_phone_start { get; set; }
        public string supervisor_arrival { get; set; }
        public string supervisor_phone_arrival { get; set; }
        public string flight_route { get; set; }
        public string attach_map { get; set; }
        public string attach_directions { get; set; }
        public string attach_other { get; set; }
        public string userID { get; set; }
    }
}
