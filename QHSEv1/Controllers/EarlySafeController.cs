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
    public class EarlySafeController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.early_safe_return_wp.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(early_safe_return_wp early_safe_return_wp)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.early_safe_return_wp.Add(early_safe_return_wp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(early_safe_return_wp);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            early_safe_return_wp early_safe_return_wp = db.early_safe_return_wp.Find(id);
            if (early_safe_return_wp == null)
            {
                return HttpNotFound();
            }
            return View(early_safe_return_wp);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(early_safe_return_wp early_safe_return_wp)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    early_safe_return_wp.id = early_safe_return_wp.id;
                    db.Entry(early_safe_return_wp).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Early Safe System", JsonRequestBehavior.AllowGet);


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
            early_safe_return_wp early_safe_return_wp = db.early_safe_return_wp.Find(id);
            if (early_safe_return_wp == null)
            {
                return HttpNotFound();
            }



            var early_safe_return_wpRow = db.early_safe_return_wp.Find(id);

            db.early_safe_return_wp.Remove(early_safe_return_wpRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Geta all records function
        public JsonResult GetAllEarlySafe()
        {

            var db = new qhsedbEntities();
            var data = db.early_safe_return_wp.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetEarlySafeById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var EarlySafeId = Convert.ToInt32(id);
                var data = db.early_safe_return_wp.Find(EarlySafeId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetEarlySafeByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.early_safe_return_wp
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new EarlySafe()
                            {

                                id = p.id,
                                plan_type = p.plan_type,
                                incident_ref_no = p.incident_ref_no,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                position = p.position,
                                claim_no = p.claim_no,
                                start_date = p.start_date,
                                ancipated_date = p.ancipated_date,
                                worker_phone_no = p.worker_phone_no,
                                worker_email_addr = p.worker_email_addr,
                                progress_report_req = p.progress_report_req,
                                injury_details = p.injury_details,
                                supervisor = p.supervisor,
                                goals = p.goals,
                                objective = p.objective,
                                plan_dev_coop = p.plan_dev_coop,
                                worker_restrictions = p.worker_restrictions,
                                details_on_worker = p.details_on_worker,
                                duties = p.duties,
                                hours = p.hours,
                                worker = p.worker,
                                manager = p.manager,
                                return_to_work = p.return_to_work,
                                attachment_1 = p.attachment_1,
                                attachment_2 = p.attachment_2,
                                attachment_3 = p.attachment_3,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.date_of_entry > fromdate && p.date_of_entry < todate));
                }
                else
                {
                    data = data.Where(p => (p.date_of_entry < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.earlySafeDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
