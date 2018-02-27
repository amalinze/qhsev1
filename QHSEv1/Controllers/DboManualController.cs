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
    public class DboManualController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.dbo_manual.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(dbo_manual dbo_manual)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.dbo_manual.Add(dbo_manual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbo_manual);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            dbo_manual dbo_manual = db.dbo_manual.Find(id);
            if (dbo_manual == null)
            {
                return HttpNotFound();
            }
            return View(dbo_manual);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(dbo_manual dbo_manual)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    dbo_manual.id = dbo_manual.id;
                    db.Entry(dbo_manual).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Manual", JsonRequestBehavior.AllowGet);


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
            dbo_manual dbo_manual = db.dbo_manual.Find(id);
            if (dbo_manual == null)
            {
                return HttpNotFound();
            }



            var dbo_manualRow = db.dbo_manual.Find(id);

            db.dbo_manual.Remove(dbo_manualRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Geta all records function
        public JsonResult GetAllDboManual()
        {

            var db = new qhsedbEntities();
            var data = db.dbo_manual.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetDboManualById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var DboManualId = Convert.ToInt32(id);
                var data = db.dbo_manual.Find(DboManualId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetDboManualByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.dbo_manual
                                .OrderBy(c => c.id)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new DboManual()
                            {

                                id = p.id,
                                dbo_manual_date = p.dbo_manual_date,
                                development_date = p.development_date,
                                revision_date = p.revision_date,
                                next_revision_date = p.next_revision_date,
                                version_number = p.version_number,
                                update_number = p.update_number,
                                revised_by = p.revised_by,
                                approved_by = p.approved_by,
                                attach_dbo_manual = p.attach_dbo_manual,
                                attach_other = p.attach_other,
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
                    m.dboManualDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
