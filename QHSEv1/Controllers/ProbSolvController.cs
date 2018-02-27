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
    public class ProbSolvController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.prob_solv_meet_rep.ToList());
        }
        [HttpPost]
        public JsonResult AddProbSolv(prob_solv_meet_rep ProbSolvObj)
        {
            try
            {
                ProbSolvObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.prob_solv_meet_rep.Add(ProbSolvObj);
                db.SaveChanges();




                return Json("Added New ProbSolv Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(prob_solv_meet_rep prob_solv_meet_rep)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.prob_solv_meet_rep.Add(prob_solv_meet_rep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prob_solv_meet_rep);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            prob_solv_meet_rep prob_solv_meet_rep = db.prob_solv_meet_rep.Find(id);
            if (prob_solv_meet_rep == null)
            {
                return HttpNotFound();
            }
            return View(prob_solv_meet_rep);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(prob_solv_meet_rep ProbSolvObj)
        {
            try
            {
                ProbSolvObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    ProbSolvObj.id = ProbSolvObj.id;
                    db.Entry(ProbSolvObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited ProbSolv System", JsonRequestBehavior.AllowGet);


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
            prob_solv_meet_rep prob_solv_meet_rep = db.prob_solv_meet_rep.Find(id);
            if (prob_solv_meet_rep == null)
            {
                return HttpNotFound();
            }



            var ProbSolvRow = db.prob_solv_meet_rep.Find(id);

            db.prob_solv_meet_rep.Remove(ProbSolvRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delProbSolv(int id)

        {
            var db = new qhsedbEntities();
            var ProbSolvRow = db.prob_solv_meet_rep.Find(id);

            db.prob_solv_meet_rep.Remove(ProbSolvRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllProbSolv()
        {

            var db = new qhsedbEntities();
            var data = db.prob_solv_meet_rep.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetProbSolvById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var ProbSolvId = Convert.ToInt32(id);
                var data = db.prob_solv_meet_rep.Find(ProbSolvId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetProbSolvByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.prob_solv_meet_rep
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new ProbSolv()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                meeting_leader = p.meeting_leader,
                                problem = p.problem,
                                analysis = p.analysis,
                                corrective_action = p.corrective_action,
                                follow_up = p.follow_up,
                                other_PAA = p.other_PAA,
                                attendance = p.attendance,
                                reviewed_by = p.reviewed_by,
                                attachment_1 = p.attachment_1,
                                attchment_2 = p.attchment_2,
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
                    m.probSolvDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
