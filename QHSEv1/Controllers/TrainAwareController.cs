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
    public class TrainAwareController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.train_aware_comp.ToList());
        }
        [HttpPost]
        public JsonResult AddTrainAware(train_aware_comp TrainAwareObj)
        {
            try
            {
                TrainAwareObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.train_aware_comp.Add(TrainAwareObj);
                db.SaveChanges();




                return Json("Added New TrainAware Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(train_aware_comp train_aware_comp)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.train_aware_comp.Add(train_aware_comp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(train_aware_comp);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            train_aware_comp train_aware_comp = db.train_aware_comp.Find(id);
            if (train_aware_comp == null)
            {
                return HttpNotFound();
            }
            return View(train_aware_comp);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(train_aware_comp TrainAwareObj)
        {
            try
            {
                TrainAwareObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    TrainAwareObj.id = TrainAwareObj.id;
                    db.Entry(TrainAwareObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }


                return Json("Edited TrainAware System", JsonRequestBehavior.AllowGet);


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
            train_aware_comp train_aware_comp = db.train_aware_comp.Find(id);
            if (train_aware_comp == null)
            {
                return HttpNotFound();
            }



            var TrainAwareRow = db.train_aware_comp.Find(id);

            db.train_aware_comp.Remove(TrainAwareRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllTrainAware()
        {

            var db = new qhsedbEntities();
            var data = db.train_aware_comp.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetTrainAwareById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var TrainAwareId = Convert.ToInt32(id);
                var data = db.train_aware_comp.Find(TrainAwareId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetTrainAwareByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.train_aware_comp
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new TrainAware()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                project_name = p.project_name,
                                work_area = p.work_area,
                                position = p.position,
                                train_obtain = p.train_obtain,
                                train_prov = p.train_prov,
                                certificat_number = p.certificat_number,
                                issue_date = p.issue_date,
                                expiry_date = p.expiry_date,
                                cert_on_file = p.cert_on_file,
                                notes = p.notes,
                                certificate = p.certificate,
                                attachment_1 = p.attachment_1,
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
                    m.trainAwareDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
