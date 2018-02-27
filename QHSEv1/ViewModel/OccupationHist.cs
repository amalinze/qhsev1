using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class OccupationHist
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string occupationHistDateStr { get; set; }
        public string name { get; set; }
        public string work_department { get; set; }
        public string position { get; set; }
        public Nullable<System.DateTime> from_date { get; set; }
        public Nullable<System.DateTime> to_date { get; set; }
        public string job_title { get; set; }
        public string physical { get; set; }
        public string chemical { get; set; }
        public string biological { get; set; }
        public string psychological { get; set; }
        public string PPE { get; set; }
        public string secondary_work { get; set; }
        public string hobbies_sports { get; set; }
        public string work_related_exp { get; set; }
        public string reproductive { get; set; }
        public string userID { get; set; }
    }
}