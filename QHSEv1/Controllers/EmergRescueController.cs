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
    public class EmergRescueController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.emerg_rescue_plan.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(emerg_rescue_plan emerg_rescue_plan)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.emerg_rescue_plan.Add(emerg_rescue_plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emerg_rescue_plan);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            emerg_rescue_plan emerg_rescue_plan = db.emerg_rescue_plan.Find(id);
            if (emerg_rescue_plan == null)
            {
                return HttpNotFound();
            }
            return View(emerg_rescue_plan);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(emerg_rescue_plan emerg_rescue_plan)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    emerg_rescue_plan.id = emerg_rescue_plan.id;
                    db.Entry(emerg_rescue_plan).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Emergency Rescue System", JsonRequestBehavior.AllowGet);


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
            emerg_rescue_plan emerg_rescue_plan = db.emerg_rescue_plan.Find(id);
            if (emerg_rescue_plan == null)
            {
                return HttpNotFound();
            }



            var emerg_rescue_planRow = db.emerg_rescue_plan.Find(id);

            db.emerg_rescue_plan.Remove(emerg_rescue_planRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllEmergRescue()
        {

            var db = new qhsedbEntities();
            var data = db.emerg_rescue_plan.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetEmergRescueById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var EmergRescueId = Convert.ToInt32(id);
                var data = db.emerg_rescue_plan.Find(EmergRescueId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetEmergRescueByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.emerg_rescue_plan
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new EmergRescue()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                plan_desc = p.plan_desc,
                                dev_date = p.dev_date,
                                next_rev_date = p.next_rev_date,
                                notes = p.notes,
                                attachment_1 = p.attachment_1,
                                attachment_2 = p.attachment_2,
                                attachment_3 = p.attachment_3,
                                attachment_4 = p.attachment_4,
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
                    m.emergRescueDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
