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
    public class NextofKinController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.next_of_kin.ToList());
        }
        [HttpPost]
        public JsonResult AddNextofKin(next_of_kin NextofKinObj)
        {
            try
            {
                var db = new qhsedbEntities();
                db.next_of_kin.Add(NextofKinObj);
                db.SaveChanges();




                return Json("Added New NextofKin Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(next_of_kin next_of_kin)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.next_of_kin.Add(next_of_kin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(next_of_kin);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            next_of_kin next_of_kin = db.next_of_kin.Find(id);
            if (next_of_kin == null)
            {
                return HttpNotFound();
            }
            return View(next_of_kin);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(next_of_kin NextofKinObj)
        {
            try
            {
                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    NextofKinObj.id = NextofKinObj.id;
                    db.Entry(NextofKinObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited NextofKin System", JsonRequestBehavior.AllowGet);


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
            next_of_kin next_of_kin = db.next_of_kin.Find(id);
            if (next_of_kin == null)
            {
                return HttpNotFound();
            }



            var NextofKinRow = db.next_of_kin.Find(id);

            db.next_of_kin.Remove(NextofKinRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delNextofKin(int id)

        {
            var db = new qhsedbEntities();
            var NextofKinRow = db.next_of_kin.Find(id);

            db.next_of_kin.Remove(NextofKinRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllNextofKin()
        {

            var db = new qhsedbEntities();
            var data = db.next_of_kin.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetNextofKinById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var NextofKinId = Convert.ToInt32(id);
                var data = db.next_of_kin.Find(NextofKinId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetNextofKinByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.next_of_kin
                                .OrderBy(c => c.id)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new NextofKin()
                            {

                                id = p.id,
                                kin_full_name = p.kin_full_name,
                                gender = p.gender,
                                relationship = p.relationship,
                                description = p.description,
                                home_phone = p.home_phone,
                                cell_phone = p.cell_phone,
                                email_address = p.email_address,
                                street_address = p.street_address,
                                city = p.city,
                                postal_code = p.postal_code,
                                state_province = p.state_province,
                                country = p.country,
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
                    m.nextofKinDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
