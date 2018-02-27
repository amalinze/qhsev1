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
    public class OccupationHistController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.occupational_history_form.ToList());
        }
        [HttpPost]
        public JsonResult AddOccupationHist(occupational_history_form OccupationHistObj)
        {
            try
            {
                OccupationHistObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.occupational_history_form.Add(OccupationHistObj);
                db.SaveChanges();




                return Json("Added New OccupationHist Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(occupational_history_form occupational_history_form)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.occupational_history_form.Add(occupational_history_form);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(occupational_history_form);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            occupational_history_form occupational_history_form = db.occupational_history_form.Find(id);
            if (occupational_history_form == null)
            {
                return HttpNotFound();
            }
            return View(occupational_history_form);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(occupational_history_form OccupationHistObj)
        {
            try
            {
                OccupationHistObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    OccupationHistObj.id = OccupationHistObj.id;
                    db.Entry(OccupationHistObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited OccupationHist System", JsonRequestBehavior.AllowGet);


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
            occupational_history_form occupational_history_form = db.occupational_history_form.Find(id);
            if (occupational_history_form == null)
            {
                return HttpNotFound();
            }



            var OccupationHistRow = db.occupational_history_form.Find(id);

            db.occupational_history_form.Remove(OccupationHistRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Delete Function
        [HttpDelete]

        public JsonResult delOccupationHist(int id)

        {
            var db = new qhsedbEntities();
            var OccupationHistRow = db.occupational_history_form.Find(id);

            db.occupational_history_form.Remove(OccupationHistRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllOccupationHist()
        {

            var db = new qhsedbEntities();
            var data = db.occupational_history_form.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetOccupationHistById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var OccupationHistId = Convert.ToInt32(id);
                var data = db.occupational_history_form.Find(OccupationHistId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetOccupationHistByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.occupational_history_form
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new OccupationHist()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                work_department = p.work_department,
                                position = p.position,
                                from_date = p.from_date,
                                to_date = p.to_date,
                                job_title = p.job_title,
                                physical = p.physical,
                                chemical = p.chemical,
                                biological = p.biological,
                                PPE = p.PPE,
                                secondary_work = p.secondary_work,
                                hobbies_sports = p.hobbies_sports,
                                work_related_exp = p.work_related_exp,
                                reproductive = p.reproductive,
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
                    m.occupationHistDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
