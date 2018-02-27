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
    public class MeetingPlanController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.meeting_planning_work.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(meeting_planning_work meeting_planning_work)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.meeting_planning_work.Add(meeting_planning_work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meeting_planning_work);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            meeting_planning_work meeting_planning_work = db.meeting_planning_work.Find(id);
            if (meeting_planning_work == null)
            {
                return HttpNotFound();
            }
            return View(meeting_planning_work);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(meeting_planning_work meeting_planning_work)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    meeting_planning_work.id = meeting_planning_work.id;
                    db.Entry(meeting_planning_work).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Meeting Plan System", JsonRequestBehavior.AllowGet);


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
            meeting_planning_work meeting_planning_work = db.meeting_planning_work.Find(id);
            if (meeting_planning_work == null)
            {
                return HttpNotFound();
            }



            var meeting_planning_workRow = db.meeting_planning_work.Find(id);

            db.meeting_planning_work.Remove(meeting_planning_workRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllMeetingPlan()
        {

            var db = new qhsedbEntities();
            var data = db.meeting_planning_work.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetMeetingPlanById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var MeetingPlanId = Convert.ToInt32(id);
                var data = db.meeting_planning_work.Find(MeetingPlanId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetMeetingPlanByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.meeting_planning_work
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new MeetingPlan()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                dt_meeting = p.dt_meeting,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                meeting_subject = p.meeting_subject,
                                meeting_description = p.meeting_description,
                                required_attendance = p.required_attendance,
                                gen_obj_meting = p.gen_obj_meting,
                                desired_end_result = p.desired_end_result,
                                planning_detail = p.planning_detail,
                                meeting_prep_checklist = p.meeting_prep_checklist,
                                attacchment_1 = p.attacchment_1,
                                attacment = p.attacment,
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
                    m.meetingPlanDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
