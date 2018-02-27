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
    public class EmergPrepController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.emerg_prep_practice.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(emerg_prep_practice emerg_prep_practice)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.emerg_prep_practice.Add(emerg_prep_practice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emerg_prep_practice);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            emerg_prep_practice emerg_prep_practice = db.emerg_prep_practice.Find(id);
            if (emerg_prep_practice == null)
            {
                return HttpNotFound();
            }
            return View(emerg_prep_practice);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(emerg_prep_practice emerg_prep_practice)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    emerg_prep_practice.id = emerg_prep_practice.id;
                    db.Entry(emerg_prep_practice).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Emergency Prepartory System", JsonRequestBehavior.AllowGet);


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
            emerg_prep_practice emerg_prep_practice = db.emerg_prep_practice.Find(id);
            if (emerg_prep_practice == null)
            {
                return HttpNotFound();
            }



            var emerg_prep_practiceRow = db.emerg_prep_practice.Find(id);

            db.emerg_prep_practice.Remove(emerg_prep_practiceRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //get record by parameter
        public JsonResult GetEmergPrepById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var EmergPrepId = Convert.ToInt32(id);
                var data = db.emerg_prep_practice.Find(EmergPrepId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetEmergPrepByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.emerg_prep_practice
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new EmergPrep()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                location = p.location,
                                dt_of_practice = p.dt_of_practice,
                                description = p.description,
                                response_time = p.response_time,
                                evaluation_notes = p.evaluation_notes,
                                corr_action_recomm = p.corr_action_recomm,
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
                    m.emergPrepDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
