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
    public class ReturnPlanController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.return_to_work_plan.ToList());
        }
        [HttpPost]
        public JsonResult AddReturnPlan(return_to_work_plan ReturnPlanObj)
        {
            try
            {
                ReturnPlanObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.return_to_work_plan.Add(ReturnPlanObj);
                db.SaveChanges();




                return Json("Added New ReturnPlan Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(return_to_work_plan return_to_work_plan)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.return_to_work_plan.Add(return_to_work_plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(return_to_work_plan);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            return_to_work_plan return_to_work_plan = db.return_to_work_plan.Find(id);
            if (return_to_work_plan == null)
            {
                return HttpNotFound();
            }
            return View(return_to_work_plan);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(return_to_work_plan ReturnPlanObj)
        {
            try
            {
                ReturnPlanObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    ReturnPlanObj.id = ReturnPlanObj.id;
                    db.Entry(ReturnPlanObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited ReturnPlan System", JsonRequestBehavior.AllowGet);


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
            return_to_work_plan return_to_work_plan = db.return_to_work_plan.Find(id);
            if (return_to_work_plan == null)
            {
                return HttpNotFound();
            }



            var ReturnPlanRow = db.return_to_work_plan.Find(id);

            db.return_to_work_plan.Remove(ReturnPlanRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delReturnPlan(int id)

        {
            var db = new qhsedbEntities();
            var ReturnPlanRow = db.return_to_work_plan.Find(id);

            db.return_to_work_plan.Remove(ReturnPlanRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllReturnPlan()
        {

            var db = new qhsedbEntities();
            var data = db.return_to_work_plan.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetReturnPlanById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var ReturnPlanId = Convert.ToInt32(id);
                var data = db.return_to_work_plan.Find(ReturnPlanId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetReturnPlanByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.return_to_work_plan
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new ReturnPlan()
                            {

                                id = p.id,
                                incident_ref_no =p.incident_ref_no,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                project_name = p.project_name,
                                work_dept  = p.work_dept,
                                position = p.position,
                                claim_no = p.claim_no,
                                supevisor = p.supevisor,
                                RTW_coordinator = p.RTW_coordinator,
                                week_no = p.week_no,
                                objectives = p.objectives,
                                limitations = p.limitations,
                                duties = p.duties,
                                hours = p.hours,
                                employee_comm_con = p.employee_comm_con,
                                action_to_addr = p.action_to_addr,
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
                    m.returnPlanDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
