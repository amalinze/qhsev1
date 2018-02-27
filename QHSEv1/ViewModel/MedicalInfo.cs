using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class MedicalInfo
    {

        public int id { get; set; }
        public string fullname { get; set; }
        public string work_department { get; set; }
        public string position { get; set; }
        public string doctor_name { get; set; }
        public string doctor_number { get; set; }
        public string medical_condition { get; set; }
        public string medications { get; set; }
        public string emergency_instructions { get; set; }
        public string attachments { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string medicalInfoDateStr { get; set; }
        public string userID { get; set; }
    }
}