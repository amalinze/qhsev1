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
    public class MonthlySafetyController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.monthly_safety_summary.ToList());
        }
        [HttpPost]
        public JsonResult AddMonthlySafety(monthly_safety_summary MonthlySafetyObj)
        {
            try
            {
                MonthlySafetyObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.monthly_safety_summary.Add(MonthlySafetyObj);
                db.SaveChanges();




                return Json("Added New MonthlySafety Entry Successfully", JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(monthly_safety_summary monthly_safety_summary)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.monthly_safety_summary.Add(monthly_safety_summary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monthly_safety_summary);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            monthly_safety_summary monthly_safety_summary = db.monthly_safety_summary.Find(id);
            if (monthly_safety_summary == null)
            {
                return HttpNotFound();
            }
            return View(monthly_safety_summary);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(monthly_safety_summary MonthlySafetyObj)
        {
            try
            {
                MonthlySafetyObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    MonthlySafetyObj.id = MonthlySafetyObj.id;
                    db.Entry(MonthlySafetyObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited MonthlySafety System", JsonRequestBehavior.AllowGet);


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
            monthly_safety_summary monthly_safety_summary = db.monthly_safety_summary.Find(id);
            if (monthly_safety_summary == null)
            {
                return HttpNotFound();
            }



            var MonthlySafetyRow = db.monthly_safety_summary.Find(id);

            db.monthly_safety_summary.Remove(MonthlySafetyRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllMonthlySafety()
        {

            var db = new qhsedbEntities();
            var data = db.monthly_safety_summary.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetMonthlySafetyById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var MonthlySafetyId = Convert.ToInt32(id);
                var data = db.monthly_safety_summary.Find(MonthlySafetyId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetMonthlySafetyByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.monthly_safety_summary
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new MonthlySafety()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                project_name = p.project_name,
                                for_month = p.for_month,
                                for_year = p.for_year,
                                inspections = p.inspections,
                                internal_audits = p.internal_audits,
                                mgmt_safety_visit = p.mgmt_safety_visit,
                                emerg_res_drill = p.emerg_res_drill,
                                safety_orientation = p.safety_orientation,
                                JSA = p.JSA,
                                remedial_actions = p.remedial_actions,
                                training_hours = p.training_hours,
                                work_refusals = p.work_refusals,
                                tailgate_toolbox = p.tailgate_toolbox,
                                gen_safety_meeting = p.gen_safety_meeting,
                                dept_area = p.dept_area,
                                mgmt_review = p.mgmt_review,
                                fatalities = p.fatalities,
                                lost_time_inc = p.lost_time_inc,
                                near_misses = p.near_misses,
                                equipment_damage = p.equipment_damage,
                                equipment_losses = p.equipment_losses,
                                environment_dist = p.environment_dist,
                                attachment = p.attachment,
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
                    m.monthlySafetyDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
