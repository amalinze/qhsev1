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
    public class HazardNmController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.hazard_nm_fin_rep.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(hazard_nm_fin_rep hazard_nm_fin_rep)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.hazard_nm_fin_rep.Add(hazard_nm_fin_rep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hazard_nm_fin_rep);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            hazard_nm_fin_rep hazard_nm_fin_rep = db.hazard_nm_fin_rep.Find(id);
            if (hazard_nm_fin_rep == null)
            {
                return HttpNotFound();
            }
            return View(hazard_nm_fin_rep);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(hazard_nm_fin_rep hazard_nm_fin_rep)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    hazard_nm_fin_rep.id = hazard_nm_fin_rep.id;
                    db.Entry(hazard_nm_fin_rep).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Hazard NM System", JsonRequestBehavior.AllowGet);


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
            hazard_nm_fin_rep hazard_nm_fin_rep = db.hazard_nm_fin_rep.Find(id);
            if (hazard_nm_fin_rep == null)
            {
                return HttpNotFound();
            }



            var hazard_nm_fin_repRow = db.hazard_nm_fin_rep.Find(id);

            db.hazard_nm_fin_rep.Remove(hazard_nm_fin_repRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllHarzardNm()
        {

            var db = new qhsedbEntities();
            var data = db.hazard_nm_fin_rep.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetHarzardNmById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var HarzardNmId = Convert.ToInt32(id);
                var data = db.hazard_nm_fin_rep.Find(HarzardNmId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetHarzardNmByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.hazard_nm_fin_rep
                                .OrderBy(c => c.date_of_event)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new HarzardNm()
                            {

                                id = p.id,
                                incident_ref = p.incident_ref,
                                event_status = p.event_status,
                                event_type = p.event_type,
                                date_of_event = p.date_of_event,
                                report_date = p.report_date,
                                project_name = p.project_name,
                                brief_desc = p.brief_desc,
                                detailed_desc = p.detailed_desc,
                                observe_comments = p.observe_comments,
                                recommended_cor_actions = p.recommended_cor_actions,
                                immediate_cor_actions = p.immediate_cor_actions,
                                violation_of_rules = p.violation_of_rules,
                                motor_veh_inc = p.motor_veh_inc,
                                immediate_action = p.immediate_action,
                                location = p.location,
                                exposure_freequency = p.exposure_freequency,
                                eyewitness_statement = p.eyewitness_statement,
                                risk_of_injury = p.risk_of_injury,
                                probability = p.probability,
                                investigation_attached = p.investigation_attached,
                                investigation_report = p.investigation_report,
                                other_attachment = p.other_attachment,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.date_of_event > fromdate && p.date_of_event < todate));
                }
                else
                {
                    data = data.Where(p => (p.date_of_event < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.hazardNmDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_event).Month + "/" + Convert.ToDateTime(m.date_of_event).Day + "/" + Convert.ToDateTime(m.date_of_event).Year);


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
