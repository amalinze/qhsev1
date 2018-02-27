using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class LegalOther
    {
        public int id { get; set; }
        public Nullable<System.DateTime> legal_other_requierments_date { get; set; }
        public string legalOtherDateStr { get; set; }
        public string description_of_requirements { get; set; }
        public string notes { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string attach_requirements { get; set; }
        public string userID { get; set; }
    }
}