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
    public class MedicalInfoController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.medicals.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(medical medical)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.medicals.Add(medical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medical);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            medical medical = db.medicals.Find(id);
            if (medical == null)
            {
                return HttpNotFound();
            }
            return View(medical);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(medical medical)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    medical.id = medical.id;
                    db.Entry(medical).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Medical System", JsonRequestBehavior.AllowGet);


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
            medical medical = db.medicals.Find(id);
            if (medical == null)
            {
                return HttpNotFound();
            }



            var medicalRow = db.medicals.Find(id);

            db.medicals.Remove(medicalRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllMedicalInfo()
        {

            var db = new qhsedbEntities();
            var data = db.medicals.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetMedicalInfoById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var MedicalInfoId = Convert.ToInt32(id);
                var data = db.medicals.Find(MedicalInfoId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetMedicalInfoByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.medicals
                                .OrderBy(c => c.id)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new MedicalInfo()
                            {

                                id = p.id,
                                fullname = p.fullname,
                                work_department = p.work_department,
                                position = p.position,
                                doctor_name = p.doctor_name,
                                doctor_number = p.doctor_number,
                                medical_condition = p.medical_condition,
                                medications = p.medications,
                                emergency_instructions = p.emergency_instructions,
                                attachments = p.attachments,
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
                    m.medicalInfoDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
