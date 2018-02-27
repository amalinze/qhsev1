using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class HazardSub
    {
        public int id { get; set; }
        public string incident_ref_no { get; set; }
        public Nullable<System.DateTime> date_of_entry { get; set; }
        public string hazardSubDateStr { get; set; }
        public string name { get; set; }
        public string project_name { get; set; }
        public string work_dept { get; set; }
        public string position { get; set; }
        public Nullable<System.DateTime> dt_of_exposure { get; set; }
        public string dur_of_exposure { get; set; }
        public string loc_of_exposure { get; set; }
        public string chemical_name { get; set; }
        public string chemical_abstract { get; set; }
        public string trad_name_chem { get; set; }
        public string type_of_exposure { get; set; }
        public string if_contact_WPBI { get; set; }
        public string how_did_expo_occur { get; set; }
        public string was_PPE_avail { get; set; }
        public string was_PPE_used { get; set; }
        public string if_PPE { get; set; }
        public string was_PTI { get; set; }
        public string were_any_symp { get; set; }
        public string if_so_desc { get; set; }
        public string severity_of_exposure { get; set; }
        public string desc_sev_of_exposure { get; set; }
        public string long_time_from_work { get; set; }
        public string est_lost_time { get; set; }
        public string list_sug { get; set; }
        public string supervisor { get; set; }
        public string MSDS { get; set; }
        public string physicians_report { get; set; }
        public string invest_report { get; set; }
        public string other_attachment { get; set; }
        public string userID { get; set; }
    }
}