using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QHSEv1.ViewModel
{
    public class DetailedIncident
    {
            public int id { get; set; }
            public string type_of_incident { get; set; }
            public string if_other_explain { get; set; }
            public string type_of_loss { get; set; }
            public string explain_loss { get; set; }
            public string name_of_company { get; set; }
            public string project_name { get; set; }
            public Nullable<System.DateTime> report_date { get; set; }
            public Nullable<System.DateTime> dt_of_incident { get; set; }
            public Nullable<System.DateTime> date_of_update { get; set; }
            public string incident_ref { get; set; }
            public string name_of_project { get; set; }
            public string project_location { get; set; }
            public string work_department { get; set; }
            public string incident_location { get; set; }
            public string name_of_supervisor { get; set; }
            public string first_worker_fn { get; set; }
            public string first_worker_position { get; set; }
            public string first_worker_est_RTW { get; set; }
            public string first_worker_cur_cond { get; set; }
            public string first_worker_sym_disc { get; set; }
            public string second_worker_fn { get; set; }
            public string second_worker_position { get; set; }
            public string second_worker_est_RTW { get; set; }
            public string second_worker_cur_cond { get; set; }
            public string second_worker_sym_disc { get; set; }
            public string return_to_work { get; set; }
            public string explanation_RTW { get; set; }
            public string incident_classification { get; set; }
            public string incident_classification_env { get; set; }
            public string incident_classification_sec { get; set; }
            public string brief_desc_of_inc { get; set; }
            public string lead_investigator { get; set; }
            public string names_of_others_II { get; set; }
            public string names_and_desc_TPI { get; set; }
            public string incident_stmt { get; set; }
            public string sequence_of_event { get; set; }
            public string specific_hazard_APPC { get; set; }
            public string comments { get; set; }
            public string body_part_affected { get; set; }
            public string incident_factor { get; set; }
            public string nature_of_burns { get; set; }
            public string planning { get; set; }
            public string explanation_planning { get; set; }
            public string worker_aware_DR { get; set; }
            public string if_unaware_explain { get; set; }
            public string safe_work_PP { get; set; }
            public string explanation_SWPP { get; set; }
            public string tools_and_equip { get; set; }
            public string work_design { get; set; }
            public string explanation_work_design { get; set; }
            public string knowledge_skill { get; set; }
            public string explanation_knowledge { get; set; }
            public string capabilities { get; set; }
            public string explanation_capabilities { get; set; }
            public string judgement { get; set; }
            public string explanation_judgement { get; set; }
            public string communication { get; set; }
            public string comments_communication { get; set; }
            public string natural_factors { get; set; }
            public string explanation_natural_factors { get; set; }
            public string reasonable_cause_TC { get; set; }
            public string immediate_action_taken { get; set; }
            public string immediate_action_taken_by { get; set; }
            public string corrective_action_taken { get; set; }
            public string accountabilities_for_CA { get; set; }
            public string safety_comm_rec { get; set; }
            public string root_cause_analysis { get; set; }
            public string snr_mng_sig_off { get; set; }
            public Nullable<System.DateTime> snr_mng_sig_off_date { get; set; }
            public string dbo_mng_sig_off { get; set; }
            public string supervisor_sig_off { get; set; }
            public string supervisor_sig_off_date { get; set; }
            public string first_wit_stmt { get; set; }
            public string second_wit_stmt { get; set; }
            public string third_wit_stmt { get; set; }
            public string photo_drawing_1 { get; set; }
            public string photo_drawing_2 { get; set; }
            public string photo_drawing_3 { get; set; }
            public string photo_drawing_4 { get; set; }
            public string photo_drawing_5 { get; set; }
            public string other_1 { get; set; }
            public string other_2 { get; set; }
           public string detailedIncidentDateStr { get; set; }
        public string userID { get; set; }
    }
    }
