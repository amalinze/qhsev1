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
    public class MgmtRevMinController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.mgmt_rev_minute.ToList());
        }
        [HttpPost]
        public JsonResult AddMgmtRevMin(mgmt_rev_minute MgmtRevMinObj)
        {
            try
            {
                MgmtRevMinObj.dt_of_meeting = DateTime.Now;
                var db = new qhsedbEntities();
                db.mgmt_rev_minute.Add(MgmtRevMinObj);
                db.SaveChanges();




                return Json("Added New MgmtRevMin Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(mgmt_rev_minute mgmt_rev_minute)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.mgmt_rev_minute.Add(mgmt_rev_minute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mgmt_rev_minute);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            mgmt_rev_minute mgmt_rev_minute = db.mgmt_rev_minute.Find(id);
            if (mgmt_rev_minute == null)
            {
                return HttpNotFound();
            }
            return View(mgmt_rev_minute);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(mgmt_rev_minute MgmtRevMinObj)
        {
            try
            {
                MgmtRevMinObj.dt_of_meeting = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    MgmtRevMinObj.id = MgmtRevMinObj.id;
                    db.Entry(MgmtRevMinObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited MgmtRevMin System", JsonRequestBehavior.AllowGet);


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
            mgmt_rev_minute mgmt_rev_minute = db.mgmt_rev_minute.Find(id);
            if (mgmt_rev_minute == null)
            {
                return HttpNotFound();
            }



            var MgmtRevMinRow = db.mgmt_rev_minute.Find(id);

            db.mgmt_rev_minute.Remove(MgmtRevMinRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllMgmtRevMin()
        {

            var db = new qhsedbEntities();
            var data = db.mgmt_rev_minute.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetMgmtRevMinById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var MgmtRevMinId = Convert.ToInt32(id);
                var data = db.mgmt_rev_minute.Find(MgmtRevMinId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetMgmtRevMinByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.mgmt_rev_minute
                                .OrderBy(c => c.dt_of_meeting)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new MgmtRevMin()
                            {

                                id = p.id,
                                dt_of_meeting = p.dt_of_meeting,
                                meeting_title = p.meeting_title,
                                obj_discussed = p.obj_discussed,
                                meeting_cllaed_by = p.meeting_cllaed_by,
                                min_sub_by = p.min_sub_by,
                                loc_of_meeting = p.loc_of_meeting,
                                project_name = p.project_name,
                                topics_discussed = p.topics_discussed,
                                desc_if_other = p.desc_if_other,
                                in_attendance = p.in_attendance,
                                meeting_top_iss = p.meeting_top_iss,
                                add_comm_ins = p.add_comm_ins,
                                sign_in_sheet = p.sign_in_sheet,
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
                    data = data.Where(p => (p.dt_of_meeting > fromdate && p.dt_of_meeting < todate));
                }
                else
                {
                    data = data.Where(p => (p.dt_of_meeting < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.mgmtRevMinDateStr = Convert.ToString(Convert.ToDateTime(m.dt_of_meeting).Month + "/" + Convert.ToDateTime(m.dt_of_meeting).Day + "/" + Convert.ToDateTime(m.dt_of_meeting).Year);


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
