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
    public class JourneyMgmtController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.journey_mgmt.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(journey_mgmt journey_mgmt)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.journey_mgmt.Add(journey_mgmt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(journey_mgmt);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            journey_mgmt journey_mgmt = db.journey_mgmt.Find(id);
            if (journey_mgmt == null)
            {
                return HttpNotFound();
            }
            return View(journey_mgmt);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(journey_mgmt journey_mgmt)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    journey_mgmt.id = journey_mgmt.id;
                    db.Entry(journey_mgmt).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Journey Mgt System", JsonRequestBehavior.AllowGet);


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
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            journey_mgmt journey_mgmt = db.journey_mgmt.Find(id);
            if (journey_mgmt == null)
            {
                return HttpNotFound();
            }



            var journey_mgmtRow = db.journey_mgmt.Find(id);

            db.journey_mgmt.Remove(journey_mgmtRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllJourneyMgmt()
        {

            var db = new qhsedbEntities();
            var data = db.journey_mgmt.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetJourneyMgmtById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var JourneyMgmtId = Convert.ToInt32(id);
                var data = db.journey_mgmt.Find(JourneyMgmtId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetJourneyMgmtByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.journey_mgmt
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new JourneyMgmt()
                            {

                                id = p.id,
                                driver_name = p.driver_name,
                                journey_status = p.journey_status,
                                vehicle_id = p.vehicle_id,
                                start_location = p.start_location,
                                destination = p.destination,
                                start_dt = p.start_dt,
                                arrival_dt = p.arrival_dt,
                                total_distance = p.total_distance,
                                total_travel_time = p.total_travel_time,
                                rest_time =p.rest_time,
                                supervisor_start = p.supervisor_start,
                                supervisor_phone_start = p.supervisor_phone_start,
                                supervisor_arrival = p.supervisor_arrival,
                                supervisor_phone_arrival = p.supervisor_phone_arrival,
                                flight_route = p.flight_route,
                                attach_map = p.attach_map,
                                attach_directions = p.attach_directions,
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
                    m.journeyMgmtDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
