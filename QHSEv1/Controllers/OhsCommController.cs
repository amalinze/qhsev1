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
    public class OhsCommController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.ohs_committee.ToList());
        }
        [HttpPost]
        public JsonResult AddOhsComm(ohs_committee OhsCommObj)
        {
            try
            {
                var db = new qhsedbEntities();
                db.ohs_committee.Add(OhsCommObj);
                db.SaveChanges();




                return Json("Added New OhsComm Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(ohs_committee ohs_committee)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.ohs_committee.Add(ohs_committee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ohs_committee);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            ohs_committee ohs_committee = db.ohs_committee.Find(id);
            if (ohs_committee == null)
            {
                return HttpNotFound();
            }
            return View(ohs_committee);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(ohs_committee OhsCommObj)
        {
            try
            {
                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    OhsCommObj.id = OhsCommObj.id;
                    db.Entry(OhsCommObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited OhsComm System", JsonRequestBehavior.AllowGet);


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
            ohs_committee ohs_committee = db.ohs_committee.Find(id);
            if (ohs_committee == null)
            {
                return HttpNotFound();
            }



            var OhsCommRow = db.ohs_committee.Find(id);

            db.ohs_committee.Remove(OhsCommRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delOhsComm(int id)

        {
            var db = new qhsedbEntities();
            var OhsCommRow = db.ohs_committee.Find(id);

            db.ohs_committee.Remove(OhsCommRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllOhsComm()
        {

            var db = new qhsedbEntities();
            var data = db.ohs_committee.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetOhsCommById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var OhsCommId = Convert.ToInt32(id);
                var data = db.ohs_committee.Find(OhsCommId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetOhsCommByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.ohs_committee
                                .OrderBy(c => c.dt_meeting)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new OhsComm()
                            {

                                id = p.id,
                                dt_meeting = p.dt_meeting,
                                meeting_title = p.meeting_title,
                                meeting_called_by = p.meeting_called_by,
                                min_sub_by = p.min_sub_by,
                                location_of_meet = p.location_of_meet,
                                project_name = p.project_name,
                                worker_co_chair = p.worker_co_chair,
                                comm_rep_cer = p.comm_rep_cer,
                                meeting_topic = p.meeting_topic,
                                add_comm_ins = p.add_comm_ins,
                                sign_in_sheet = p.sign_in_sheet,
                                attachment_1 = p.attachment_1,
                                atachment_2 = p.atachment_2,
                                attachment_3 = p.attachment_3,
                                attachment_4 = p.attachment_4,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.dt_meeting > fromdate && p.dt_meeting < todate));
                }
                else
                {
                    data = data.Where(p => (p.dt_meeting < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.ohsCommDateStr = Convert.ToString(Convert.ToDateTime(m.dt_meeting).Month + "/" + Convert.ToDateTime(m.dt_meeting).Day + "/" + Convert.ToDateTime(m.dt_meeting).Year);


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
