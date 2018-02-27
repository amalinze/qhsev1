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
    public class ContactController : Controller
    {

        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.contacts.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(contact contact)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(contact contact)
        {
            try
            {
               
                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    contact.id = contact.id;
                    db.Entry(contact).State = EntityState.Modified;

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
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }



            var contactRow = db.contacts.Find(id);

            db.contacts.Remove(contactRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //get record by parameter
        public JsonResult GetContactById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var ContactId = Convert.ToInt32(id);
                var data = db.contacts.Find(ContactId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetContactByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.contacts
                                .OrderBy(c => c.id)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new Contact()
                            {

                                id = p.id,
                                fullname = p.fullname,
                                company_name = p.company_name,
                                project_name = p.project_name,
                                department = p.department,
                                gender = p.gender,
                                work_number = p.work_number,
                                mobile_number= p.mobile_number,
                                website_url = p.website_url,
                                website_title = p.website_title,
                                attach_photo = p.attach_photo,
                                about_contact = p.about_contact,
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
                    m.contactDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
