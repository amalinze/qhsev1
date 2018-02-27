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
    public class SafeProcController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.safe_work_procedure.ToList());
        }
        [HttpPost]
        public JsonResult AddSafeProc(safe_work_procedure SafeProcObj)
        {
            try
            {
                var db = new qhsedbEntities();
                db.safe_work_procedure.Add(SafeProcObj);
                db.SaveChanges();




                return Json("Added New SafeProc Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(safe_work_procedure safe_work_procedure)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.safe_work_procedure.Add(safe_work_procedure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(safe_work_procedure);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            safe_work_procedure safe_work_procedure = db.safe_work_procedure.Find(id);
            if (safe_work_procedure == null)
            {
                return HttpNotFound();
            }
            return View(safe_work_procedure);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(safe_work_procedure SafeProcObj)
        {
            try
            {
                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    SafeProcObj.id = SafeProcObj.id;
                    db.Entry(SafeProcObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited SafeProc System", JsonRequestBehavior.AllowGet);


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
            safe_work_procedure safe_work_procedure = db.safe_work_procedure.Find(id);
            if (safe_work_procedure == null)
            {
                return HttpNotFound();
            }



            var SafeProcRow = db.safe_work_procedure.Find(id);

            db.safe_work_procedure.Remove(SafeProcRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delSafeProc(int id)

        {
            var db = new qhsedbEntities();
            var SafeProcRow = db.safe_work_procedure.Find(id);

            db.safe_work_procedure.Remove(SafeProcRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllSafeProc()
        {

            var db = new qhsedbEntities();
            var data = db.safe_work_procedure.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetSafeProcById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var SafeProcId = Convert.ToInt32(id);
                var data = db.safe_work_procedure.Find(SafeProcId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetSafeProcByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.safe_work_procedure
                                .OrderBy(c => c.development_date)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new SafeProc()
                            {

                                id = p.id,
                                safe_work_procedure_description = p.safe_work_procedure_description,
                                development_date = p.development_date,
                                version_number = p.version_number,
                                review_date = p.review_date,
                                next_review_date = p.next_review_date,
                                swp_notes = p.swp_notes,
                                attach_other = p.attach_other,
                                attach_swp = p.attach_swp,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.development_date > fromdate && p.development_date < todate));
                }
                else
                {
                    data = data.Where(p => (p.development_date < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.safeProcDateStr = Convert.ToString(Convert.ToDateTime(m.development_date).Month + "/" + Convert.ToDateTime(m.development_date).Day + "/" + Convert.ToDateTime(m.development_date).Year);


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
