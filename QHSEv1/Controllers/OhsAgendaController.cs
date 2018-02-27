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
    public class OhsAgendaController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.ohs_comm_agenda.ToList());
        }
        [HttpPost]
        public JsonResult AddOhsAgenda(ohs_comm_agenda OhsAgendaObj)
        {
            try
            {
                var db = new qhsedbEntities();
                db.ohs_comm_agenda.Add(OhsAgendaObj);
                db.SaveChanges();




                return Json("Added New OhsAgenda Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(ohs_comm_agenda ohs_comm_agenda)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.ohs_comm_agenda.Add(ohs_comm_agenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ohs_comm_agenda);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            ohs_comm_agenda ohs_comm_agenda = db.ohs_comm_agenda.Find(id);
            if (ohs_comm_agenda == null)
            {
                return HttpNotFound();
            }
            return View(ohs_comm_agenda);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(ohs_comm_agenda OhsAgendaObj)
        {
            try
            {
                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    OhsAgendaObj.id = OhsAgendaObj.id;
                    db.Entry(OhsAgendaObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited OhsAgenda System", JsonRequestBehavior.AllowGet);


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
            ohs_comm_agenda ohs_comm_agenda = db.ohs_comm_agenda.Find(id);
            if (ohs_comm_agenda == null)
            {
                return HttpNotFound();
            }



            var OhsAgendaRow = db.ohs_comm_agenda.Find(id);

            db.ohs_comm_agenda.Remove(OhsAgendaRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delOhsAgenda(int id)

        {
            var db = new qhsedbEntities();
            var OhsAgendaRow = db.ohs_comm_agenda.Find(id);

            db.ohs_comm_agenda.Remove(OhsAgendaRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllOhsAgenda()
        {

            var db = new qhsedbEntities();
            var data = db.ohs_comm_agenda.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetOhsAgendaById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var OhsAgendaId = Convert.ToInt32(id);
                var data = db.ohs_comm_agenda.Find(OhsAgendaId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetOhsAgendaByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.ohs_comm_agenda
                                .OrderBy(c => c.dt_meeting)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new OhsAgenda()
                            {

                                id = p.id,
                                dt_meeting = p.dt_meeting,
                                project_name = p.project_name,
                                total_no_emp = p.total_no_emp,
                                no_issues_def = p.no_issues_def,
                                required_attendance = p.required_attendance,
                                employer_co_chair = p.employer_co_chair,
                                worker_co_chair = p.worker_co_chair,
                                guests = p.guests,
                                no_of_workplace_con = p.no_of_workplace_con,
                                no_issues_identified = p.no_issues_identified,
                                no_complaint_received = p.no_complaint_received,
                                were_there_issues = p.were_there_issues,
                                no_acc_inv_con = p.no_acc_inv_con,
                                no_inc_inv_con = p.no_inc_inv_con,
                                were_there_any_issues = p.were_there_any_issues,
                                no_inc_report_review = p.no_inc_report_review,
                                no_acc_report_review = p.no_acc_report_review,
                                no_work_refusals = p.no_work_refusals,
                                meeting_topics_issues = p.meeting_topics_issues,
                                add_com_ins = p.add_com_ins,
                                attachment_1 = p.attachment_1,
                                attachment_2 = p.attachment_2,
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
                    m.ohsAgendaDateStr = Convert.ToString(Convert.ToDateTime(m.dt_meeting).Month + "/" + Convert.ToDateTime(m.dt_meeting).Day + "/" + Convert.ToDateTime(m.dt_meeting).Year);


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
