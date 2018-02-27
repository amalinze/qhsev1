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
    public class CompanyInfoController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.company_information.ToList());
        }
        
        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        public ActionResult Create(company_information company_information)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.company_information.Add(company_information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company_information);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            company_information company_information = db.company_information.Find(id);
            if (company_information == null)
            {
                return HttpNotFound();
            }
            return View(company_information);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(company_information CompanyInfoObj)
        {
            try
            {
                CompanyInfoObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    CompanyInfoObj.id = CompanyInfoObj.id;
                    db.Entry(CompanyInfoObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited CompanyInfo System", JsonRequestBehavior.AllowGet);


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
            company_information company_information = db.company_information.Find(id);
            if (company_information == null)
            {
                return HttpNotFound();
            }


       
            var CompanyInfoRow = db.company_information.Find(id);

            db.company_information.Remove(CompanyInfoRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllCompanyInfo()
        {

            var db = new qhsedbEntities();
            var data = db.company_information.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetCompanyInfoById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var CompanyInfoId = Convert.ToInt32(id);
                var data = db.company_information.Find(CompanyInfoId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetCompanyInfoByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.company_information
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new CompanyInfo()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                company_name = p.company_name,
                                legal_name = p.legal_name,
                                phone_number = p.phone_number,
                                alternate_phone_number = p.alternate_phone_number,
                                emergency_contact = p.emergency_contact,
                                emergency_number = p.emergency_number,
                                email = p.email,
                                city = p.city,
                                street_address = p.street_address,
                                state_province = p.state_province,
                                postal_code = p.postal_code,
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
                    m.companyInfoDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
