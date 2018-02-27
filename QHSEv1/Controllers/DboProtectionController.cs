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
    public class DboProtectionController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.dbo_protection_programs.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(dbo_protection_programs dbo_protection_programs)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.dbo_protection_programs.Add(dbo_protection_programs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbo_protection_programs);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            dbo_protection_programs dbo_protection_programs = db.dbo_protection_programs.Find(id);
            if (dbo_protection_programs == null)
            {
                return HttpNotFound();
            }
            return View(dbo_protection_programs);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(dbo_protection_programs dbo_protection_programs)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    dbo_protection_programs.id = dbo_protection_programs.id;
                    db.Entry(dbo_protection_programs).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Protection System", JsonRequestBehavior.AllowGet);


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
            dbo_protection_programs dbo_protection_programs = db.dbo_protection_programs.Find(id);
            if (dbo_protection_programs == null)
            {
                return HttpNotFound();
            }



            var dbo_protection_programsRow = db.dbo_protection_programs.Find(id);

            db.dbo_protection_programs.Remove(dbo_protection_programsRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Geta all records function
        public JsonResult GetAllDboProtection()
        {

            var db = new qhsedbEntities();
            var data = db.dbo_protection_programs.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetDboProtectionById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var DboProtectionId = Convert.ToInt32(id);
                var data = db.dbo_protection_programs.Find(DboProtectionId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetDboProtectionByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.dbo_protection_programs
                                .OrderBy(c => c.dbo_protection_programs_date)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new DboProtection()
                            {

                                id = p.id,
                                dbo_protection_programs_date = p.dbo_protection_programs_date,
                                project_name = p.project_name,
                                work_area_department = p.work_area_department,
                                description_of_program = p.description_of_program,
                                version_number = p.version_number,
                                revision_date = p.revision_date,
                                notes = p.notes,
                                attach_program = p.attach_program,
                                attach_other = p.attach_other,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.dbo_protection_programs_date > fromdate && p.dbo_protection_programs_date < todate));
                }
                else
                {
                    data = data.Where(p => (p.dbo_protection_programs_date < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.dboProtectionDateStr = Convert.ToString(Convert.ToDateTime(m.dbo_protection_programs_date).Month + "/" + Convert.ToDateTime(m.dbo_protection_programs_date).Day + "/" + Convert.ToDateTime(m.dbo_protection_programs_date).Year);


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
