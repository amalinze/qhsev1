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
    public class GeneralInfoController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.general_information.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(general_information general_information)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.general_information.Add(general_information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(general_information);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            general_information general_information = db.general_information.Find(id);
            if (general_information == null)
            {
                return HttpNotFound();
            }
            return View(general_information);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(general_information general_information)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    general_information.id = general_information.id;
                    db.Entry(general_information).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited General Info System", JsonRequestBehavior.AllowGet);


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
            general_information general_information = db.general_information.Find(id);
            if (general_information == null)
            {
                return HttpNotFound();
            }



            var general_informationRow = db.general_information.Find(id);

            db.general_information.Remove(general_informationRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //get record by parameter
        public JsonResult GetGeneralInfoById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var GeneralInfoId = Convert.ToInt32(id);
                var data = db.general_information.Find(GeneralInfoId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetGeneralInfoByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.general_information
                                .OrderBy(c => c.id)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new GeneralInfo()
                            {

                                id = p.id,
                                full_name = p.full_name,
                                currently_employed = p.currently_employed,
                                position = p.position,
                                project_name = p.project_name,
                                work_area_dept = p.work_area_dept,
                                gender = p.gender,
                                street_address = p.street_address,
                                city = p.city,
                                state_province = p.state_province,
                                postal_code = p.postal_code,
                                home_number = p.home_number,
                                cell_number = p.cell_number,
                                email_address = p.email_address,
                                medical_card_number = p.medical_card_number,
                                drivers_license_class = p.drivers_license_class,
                                medical_condition = p.medical_condition,
                                insurance_coverage = p.insurance_coverage,
                                notes = p.notes,
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
                    m.generalInfoDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
