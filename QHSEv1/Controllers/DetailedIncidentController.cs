using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using QHSEv1.Models;
using QHSEv1.ViewModel;
using System.Net;

namespace QHSEv1.Controllers
{
    public class DetailedIncidentController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.detailed_incident_invest_rep.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(detailed_incident_invest_rep detailed_incident_invest_rep)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.detailed_incident_invest_rep.Add(detailed_incident_invest_rep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detailed_incident_invest_rep);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            detailed_incident_invest_rep detailed_incident_invest_rep = db.detailed_incident_invest_rep.Find(id);
            if (detailed_incident_invest_rep == null)
            {
                return HttpNotFound();
            }
            return View(detailed_incident_invest_rep);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(detailed_incident_invest_rep detailed_incident_invest_rep)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    detailed_incident_invest_rep.id = detailed_incident_invest_rep.id;
                    db.Entry(detailed_incident_invest_rep).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Detailed Incident System", JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }





        public ActionResult Delete(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            detailed_incident_invest_rep detailed_incident_invest_rep = db.detailed_incident_invest_rep.Find(id);
            if (detailed_incident_invest_rep == null)
            {
                return HttpNotFound();
            }



            var detailed_incident_invest_repRow = db.detailed_incident_invest_rep.Find(id);

            db.detailed_incident_invest_rep.Remove(detailed_incident_invest_repRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


    
        //Geta all records function
        public JsonResult GetAllDetailedIncident()
        {

            var db = new qhsedbEntities();
            var data = db.detailed_incident_invest_rep.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetDetailedIncidentById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var DetailedIncidentId = Convert.ToInt32(id);
                var data = db.detailed_incident_invest_rep.Find(DetailedIncidentId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetDetailedIncidentByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.detailed_incident_invest_rep
                                .OrderBy(c => c.dt_of_incident)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new DetailedIncident()
                            {

                                id = p.id,
                                type_of_incident = p.type_of_incident,
                                if_other_explain = p.if_other_explain,
                                type_of_loss = p.type_of_loss,
                                explain_loss = p.explain_loss,
                                name_of_company = p.name_of_company,
                                project_name = p.project_name,
                                report_date = p.report_date,
                                dt_of_incident = p.dt_of_incident,
                                date_of_update = p.date_of_update,
                                incident_ref = p.incident_ref,
                                name_of_project = p.name_of_project,
                                project_location = p.project_location,
                                work_department = p.work_department,
                                incident_location = p.incident_location,
                                name_of_supervisor = p.name_of_supervisor,
                                first_worker_fn = p.first_worker_fn,
                                first_worker_position = p.first_worker_position,
                                first_worker_est_RTW = p.first_worker_est_RTW,
                                first_worker_cur_cond = p.first_worker_cur_cond,
                                first_worker_sym_disc = p.first_worker_sym_disc,
                                second_worker_fn = p.second_worker_fn,
                                second_worker_position = p.second_worker_position,
                                second_worker_est_RTW = p.second_worker_est_RTW,
                                second_worker_cur_cond = p.second_worker_cur_cond,
                                second_worker_sym_disc = p.second_worker_sym_disc,
                                return_to_work= p.return_to_work,
                                explanation_RTW = p.explanation_RTW,
                                incident_classification = p.incident_classification,
                                incident_classification_env = p.incident_classification_env,
                                incident_classification_sec = p.incident_classification_sec,
                                brief_desc_of_inc = p.brief_desc_of_inc,
                                lead_investigator = p.lead_investigator,
                                names_of_others_II = p.names_of_others_II,
                                names_and_desc_TPI = p.names_and_desc_TPI,
                                incident_stmt = p.incident_stmt,
                                sequence_of_event = p.sequence_of_event,
                                specific_hazard_APPC = p.specific_hazard_APPC,
                                comments = p.comments,
                                body_part_affected = p.body_part_affected,
                                incident_factor = p.incident_factor,
                                nature_of_burns = p.nature_of_burns,
                                planning = p.planning,
                                worker_aware_DR = p.worker_aware_DR,
                                if_unaware_explain = p.if_unaware_explain,
                                safe_work_PP = p.safe_work_PP,
                                explanation_SWPP = p.explanation_SWPP,
                                tools_and_equip = p.tools_and_equip,
                                work_design = p.work_design,
                                explanation_work_design = p.explanation_work_design,
                                knowledge_skill = p.knowledge_skill,
                                explanation_knowledge = p.explanation_knowledge,
                                capabilities = p.capabilities,
                                explanation_capabilities = p.explanation_capabilities,
                                judgement = p.judgement,
                                explanation_judgement = p.explanation_judgement,
                                communication = p.communication,
                                comments_communication = p.comments_communication,
                                natural_factors = p.natural_factors,
                                explanation_natural_factors = p.explanation_natural_factors,
                                reasonable_cause_TC = p.reasonable_cause_TC,
                                immediate_action_taken = p.immediate_action_taken,
                                immediate_action_taken_by = p.immediate_action_taken_by,
                                corrective_action_taken = p.corrective_action_taken,
                                accountabilities_for_CA = p.accountabilities_for_CA,
                                safety_comm_rec = p.safety_comm_rec,
                                root_cause_analysis = p.root_cause_analysis,
                                snr_mng_sig_off = p.snr_mng_sig_off,
                                snr_mng_sig_off_date = p.snr_mng_sig_off_date,
                                first_wit_stmt = p.first_wit_stmt,
                                second_wit_stmt = p.second_wit_stmt,
                                third_wit_stmt = p.third_wit_stmt,
                                photo_drawing_1 = p.photo_drawing_1,
                                photo_drawing_2 = p.photo_drawing_2,
                                photo_drawing_3 = p.photo_drawing_3,
                                photo_drawing_4 = p.photo_drawing_4,
                                photo_drawing_5 = p.photo_drawing_5,
                                other_1 = p.other_1,
                                other_2 = p.other_2,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.dt_of_incident > fromdate && p.dt_of_incident < todate));
                }
                else
                {
                    data = data.Where(p => (p.dt_of_incident < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.detailedIncidentDateStr = Convert.ToString(Convert.ToDateTime(m.dt_of_incident).Month + "/" + Convert.ToDateTime(m.dt_of_incident).Day + "/" + Convert.ToDateTime(m.dt_of_incident).Year);


                });
                return Json(dataResult, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
