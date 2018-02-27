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
    public class MgmtRevController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.mgmt_rev_agenda.ToList());
        }
        [HttpPost]
        public JsonResult AddMgmtRev(mgmt_rev_agenda MgmtRevObj)
        {
            try
            {
                MgmtRevObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.mgmt_rev_agenda.Add(MgmtRevObj);
                db.SaveChanges();




                return Json("Added New MgmtRev Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(mgmt_rev_agenda mgmt_rev_agenda)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.mgmt_rev_agenda.Add(mgmt_rev_agenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mgmt_rev_agenda);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            mgmt_rev_agenda mgmt_rev_agenda = db.mgmt_rev_agenda.Find(id);
            if (mgmt_rev_agenda == null)
            {
                return HttpNotFound();
            }
            return View(mgmt_rev_agenda);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(mgmt_rev_agenda MgmtRevObj)
        {
            try
            {
                MgmtRevObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    MgmtRevObj.id = MgmtRevObj.id;
                    db.Entry(MgmtRevObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited MgmtRev System", JsonRequestBehavior.AllowGet);


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
            mgmt_rev_agenda mgmt_rev_agenda = db.mgmt_rev_agenda.Find(id);
            if (mgmt_rev_agenda == null)
            {
                return HttpNotFound();
            }



            var MgmtRevRow = db.mgmt_rev_agenda.Find(id);

            db.mgmt_rev_agenda.Remove(MgmtRevRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllMgmtRev()
        {

            var db = new qhsedbEntities();
            var data = db.mgmt_rev_agenda.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetMgmtRevById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var MgmtRevId = Convert.ToInt32(id);
                var data = db.mgmt_rev_agenda.Find(MgmtRevId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetMgmtRevByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.mgmt_rev_agenda
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new MgmtRev()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                meeting_time = p.meeting_time,
                                objective = p.objective,
                                meeting_called_by = p.meeting_called_by,
                                dt_of_meeting = p.dt_of_meeting,
                                project_name = p.project_name,
                                location = p.location,
                                topics_of_discussion = p.topics_of_discussion,
                                desc_if_other = p.desc_if_other,
                                attendance_req_by = p.attendance_req_by,
                                spec_meeting_top = p.spec_meeting_top,
                                add_com_ins = p.add_com_ins,
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
                    data = data.Where(p => (p.date_of_entry > fromdate && p.date_of_entry < todate));
                }
                else
                {
                    data = data.Where(p => (p.date_of_entry < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.mgmtRevDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
