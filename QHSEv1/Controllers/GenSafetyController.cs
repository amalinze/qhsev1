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
    public class GenSafetyController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.general_safety_meetings.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(general_safety_meetings general_safety_meetings)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.general_safety_meetings.Add(general_safety_meetings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(general_safety_meetings);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            general_safety_meetings general_safety_meetings = db.general_safety_meetings.Find(id);
            if (general_safety_meetings == null)
            {
                return HttpNotFound();
            }
            return View(general_safety_meetings);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(general_safety_meetings general_safety_meetings)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    general_safety_meetings.id = general_safety_meetings.id;
                    db.Entry(general_safety_meetings).State = EntityState.Modified;

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
            general_safety_meetings general_safety_meetings = db.general_safety_meetings.Find(id);
            if (general_safety_meetings == null)
            {
                return HttpNotFound();
            }



            var general_safety_meetingsRow = db.general_safety_meetings.Find(id);

            db.general_safety_meetings.Remove(general_safety_meetingsRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllGenSafety()
        {

            var db = new qhsedbEntities();
            var data = db.general_safety_meetings.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetGenSafetyById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var GenSafetyId = Convert.ToInt32(id);
                var data = db.general_safety_meetings.Find(GenSafetyId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetGenSafetyByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.general_safety_meetings
                                .OrderBy(c => c.dt_of_meeting)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new GenSafety()
                            {

                                id = p.id,
                                dt_of_meeting = p.dt_of_meeting,
                                project_name = p.project_name,
                                work_area = p.work_area,
                                other_dept = p.other_dept,
                                shift = p.shift,
                                no_in_crew = p.no_in_crew,
                                no_attending = p.no_attending,
                                topic_disc = p.topic_disc,
                                employee_con_sug = p.employee_con_sug,
                                OHS_con_sug = p.OHS_con_sug,
                                attended_by = p.attended_by,
                                managers_remarks = p.managers_remarks,
                                presenter = p.presenter,
                                supervisor = p.supervisor,
                                manager = p.manager,
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
                    data = data.Where(p => (p.dt_of_meeting > fromdate && p.dt_of_meeting < todate));
                }
                else
                {
                    data = data.Where(p => (p.dt_of_meeting < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.genSafetyDateStr = Convert.ToString(Convert.ToDateTime(m.dt_of_meeting).Month + "/" + Convert.ToDateTime(m.dt_of_meeting).Day + "/" + Convert.ToDateTime(m.dt_of_meeting).Year);


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
