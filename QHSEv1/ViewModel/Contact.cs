using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class Contact
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string company_name { get; set; }
        public string project_name { get; set; }
        public string department { get; set; }
        public string gender { get; set; }
        public string work_number { get; set; }
        public string mobile_number { get; set; }
        public string website_url { get; set; }
        public string website_title { get; set; }
        public string attach_photo { get; set; }
        public string about_contact { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string contactDateStr { get; set; }
        public string userID { get; set; }
    }
}