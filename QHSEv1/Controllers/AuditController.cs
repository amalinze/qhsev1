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
    public class AuditController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.audit_guildlines.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(audit_guildlines audit_guildlines)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.audit_guildlines.Add(audit_guildlines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(audit_guildlines);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            audit_guildlines audit_guildlines = db.audit_guildlines.Find(id);
            if (audit_guildlines == null)
            {
                return HttpNotFound();
            }
            return View(audit_guildlines);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(audit_guildlines audit_guildlines)
        {
            try
            {
                audit_guildlines.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    audit_guildlines.audit_guildlines_id= audit_guildlines.audit_guildlines_id;
                    db.Entry(audit_guildlines).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
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
            audit_guildlines audit_guildlines = db.audit_guildlines.Find(id);
            if (audit_guildlines == null)
            {
                return HttpNotFound();
            }



            var CompanyInfoRow = db.audit_guildlines.Find(id);

            db.audit_guildlines.Remove(CompanyInfoRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
        //get record by parameter
        public JsonResult GetAuditById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var auditId = Convert.ToInt32(id);
                var data = db.audit_guildlines.Find(auditId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetAuditByQueryReport(string audit_guildlines_id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.audit_guildlines
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new Audit()
                            {

                                audit_guildlines_id = p.audit_guildlines_id,
                                date_of_entry = p.date_of_entry,
                                description = p.description,
                                notes = p.notes,
                                attach_audit_guildline = p.attach_audit_guildline,
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
                    m.auditDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
