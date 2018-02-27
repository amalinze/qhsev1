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
    public class DboAuditsController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.dbo_audits.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(dbo_audits dbo_audits)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.dbo_audits.Add(dbo_audits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbo_audits);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            dbo_audits dbo_audits = db.dbo_audits.Find(id);
            if (dbo_audits == null)
            {
                return HttpNotFound();
            }
            return View(dbo_audits);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(dbo_audits dbo_audits)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    dbo_audits.id = dbo_audits.id;
                    db.Entry(dbo_audits).State = EntityState.Modified;

                    db.SaveChanges();

                }



                return Json("Edited Audit System", JsonRequestBehavior.AllowGet);


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
            dbo_audits dbo_audits = db.dbo_audits.Find(id);
            if (dbo_audits == null)
            {
                return HttpNotFound();
            }



            var dbo_auditsRow = db.dbo_audits.Find(id);

            db.dbo_audits.Remove(dbo_auditsRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //get record by parameter
        public JsonResult GetDboAuditsById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var DboAuditsId = Convert.ToInt32(id);
                var data = db.dbo_audits.Find(DboAuditsId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetDboAuditsByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.dbo_audits
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new DboAudits()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                dbo_audit_date = p.dbo_audit_date,
                                type_of_audit = p.type_of_audit,
                                project_name = p.project_name,
                                description = p.description,
                                completed_by = p.completed_by,
                                auditing_organization = p.auditing_organization,
                                score = p.score,
                                notes = p.notes,
                                attach_audits = p.attach_audits,
                                attach_other = p.attach_other,
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
                    m.dboAuditsDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
