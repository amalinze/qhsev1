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
    public class DisciplinaryActionController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.disciplinary_action.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(disciplinary_action disciplinary_action)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.disciplinary_action.Add(disciplinary_action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disciplinary_action);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            disciplinary_action disciplinary_action = db.disciplinary_action.Find(id);
            if (disciplinary_action == null)
            {
                return HttpNotFound();
            }
            return View(disciplinary_action);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(disciplinary_action disciplinary_action)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    disciplinary_action.id = disciplinary_action.id;
                    db.Entry(disciplinary_action).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Disciplinary System", JsonRequestBehavior.AllowGet);


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
            disciplinary_action disciplinary_action = db.disciplinary_action.Find(id);
            if (disciplinary_action == null)
            {
                return HttpNotFound();
            }



            var disciplinary_actionRow = db.disciplinary_action.Find(id);

            db.disciplinary_action.Remove(disciplinary_actionRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Geta all records function
        public JsonResult GetAllDisciplinaryAction()
        {

            var db = new qhsedbEntities();
            var data = db.disciplinary_action.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetDisciplinaryActionById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var DisciplinaryActionId = Convert.ToInt32(id);
                var data = db.disciplinary_action.Find(DisciplinaryActionId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetDisciplinaryActionByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.disciplinary_action
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new DisciplinaryAction()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                employee_name = p.employee_name,
                                project_name = p.project_name,
                                work_area = p.work_area,
                                position = p.position,
                                company = p.company,
                                supervisor_name = p.supervisor_name,
                                action_taken = p.action_taken,
                                offences = p.offences,
                                dbo_dept_notified = p.dbo_dept_notified,
                                appr_manager_notified = p.appr_manager_notified,
                                employee_comments = p.employee_comments,
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
                    m.disciplinaryActionDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
