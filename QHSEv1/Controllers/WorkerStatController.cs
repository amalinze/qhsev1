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
    public class WorkerStatController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.worker_comp_insurace_reports_stats.ToList());
        }
        [HttpPost]
        public JsonResult AddWorkerStat(worker_comp_insurace_reports_stats WorkerStatObj)
        {
            try
            {
                 var db = new qhsedbEntities();
                db.worker_comp_insurace_reports_stats.Add(WorkerStatObj);
                db.SaveChanges();




                return Json("Added New WorkerStat Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(worker_comp_insurace_reports_stats worker_comp_insurace_reports_stats)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.worker_comp_insurace_reports_stats.Add(worker_comp_insurace_reports_stats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(worker_comp_insurace_reports_stats);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            worker_comp_insurace_reports_stats worker_comp_insurace_reports_stats = db.worker_comp_insurace_reports_stats.Find(id);
            if (worker_comp_insurace_reports_stats == null)
            {
                return HttpNotFound();
            }
            return View(worker_comp_insurace_reports_stats);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(worker_comp_insurace_reports_stats WorkerStatObj)
        {
            try
            {
                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    WorkerStatObj.id = WorkerStatObj.id;
                    db.Entry(WorkerStatObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited WorkerStat System", JsonRequestBehavior.AllowGet);


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
            worker_comp_insurace_reports_stats worker_comp_insurace_reports_stats = db.worker_comp_insurace_reports_stats.Find(id);
            if (worker_comp_insurace_reports_stats == null)
            {
                return HttpNotFound();
            }



            var WorkerStatRow = db.worker_comp_insurace_reports_stats.Find(id);

            db.worker_comp_insurace_reports_stats.Remove(WorkerStatRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delWorkerStat(int id)

        {
            var db = new qhsedbEntities();
            var WorkerStatRow = db.worker_comp_insurace_reports_stats.Find(id);

            db.worker_comp_insurace_reports_stats.Remove(WorkerStatRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllWorkerStat()
        {

            var db = new qhsedbEntities();
            var data = db.worker_comp_insurace_reports_stats.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetWorkerStatById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var WorkerStatId = Convert.ToInt32(id);
                var data = db.worker_comp_insurace_reports_stats.Find(WorkerStatId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetWorkerStatByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.worker_comp_insurace_reports_stats
                                .OrderBy(c => c.id)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new WorkerStat()
                            {

                                id = p.id,
                                worker_comp_insurace_reports_stats_date = p.worker_comp_insurace_reports_stats_date,
                                description = p.description,
                                project_name = p.project_name,
                                department_work_area = p.department_work_area,
                                details = p.details,
                                attachment_1 = p.attachment_1,
                                attachment_2 = p.attachment_2,
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
                    m.workerStatDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
