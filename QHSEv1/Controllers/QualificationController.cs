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
    public class QualificationController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.qualifications.ToList());
        }
        [HttpPost]
        public JsonResult AddQualification(qualification QualificationObj)
        {
            try
            {
                QualificationObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.qualifications.Add(QualificationObj);
                db.SaveChanges();




                return Json("Added New Qualification Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(qualification qualifications)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.qualifications.Add(qualifications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qualifications);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            qualification qualifications = db.qualifications.Find(id);
            if (qualifications == null)
            {
                return HttpNotFound();
            }
            return View(qualifications);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(qualification QualificationObj)
        {
            try
            {
                QualificationObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    QualificationObj.id = QualificationObj.id;
                    db.Entry(QualificationObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Qualification System", JsonRequestBehavior.AllowGet);


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
            qualification qualifications = db.qualifications.Find(id);
            if (qualifications == null)
            {
                return HttpNotFound();
            }



            var QualificationRow = db.qualifications.Find(id);

            db.qualifications.Remove(QualificationRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Delete Function
        [HttpDelete]

        public JsonResult delQualification(int id)

        {
            var db = new qhsedbEntities();
            var QualificationRow = db.qualifications.Find(id);

            db.qualifications.Remove(QualificationRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllQualification()
        {

            var db = new qhsedbEntities();
            var data = db.qualifications.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetQualificationById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var QualificationId = Convert.ToInt32(id);
                var data = db.qualifications.Find(QualificationId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetQualificationByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.qualifications
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new Qualification()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                work_department = p.work_department,
                                position = p.position,
                                qualification_date = p.qualification_date,
                                q_expiry_date = p.q_expiry_date,
                                qualification1 = p.qualification1,
                                notes = p.notes,
                                attach_certificate = p.attach_certificate,
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
                    m.qualificationDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
