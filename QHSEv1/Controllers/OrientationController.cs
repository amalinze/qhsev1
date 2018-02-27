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
    public class OrientationController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.orientation_tr_mat.ToList());
        }
        [HttpPost]
        public JsonResult AddOrientation(orientation_tr_mat OrientationObj)
        {
            try
            {
                OrientationObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.orientation_tr_mat.Add(OrientationObj);
                db.SaveChanges();




                return Json("Added New Orientation Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(orientation_tr_mat orientation_tr_mat)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.orientation_tr_mat.Add(orientation_tr_mat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orientation_tr_mat);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            orientation_tr_mat orientation_tr_mat = db.orientation_tr_mat.Find(id);
            if (orientation_tr_mat == null)
            {
                return HttpNotFound();
            }
            return View(orientation_tr_mat);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(orientation_tr_mat OrientationObj)
        {
            try
            {
                OrientationObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    OrientationObj.id = OrientationObj.id;
                    db.Entry(OrientationObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Orientation System", JsonRequestBehavior.AllowGet);


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
            orientation_tr_mat orientation_tr_mat = db.orientation_tr_mat.Find(id);
            if (orientation_tr_mat == null)
            {
                return HttpNotFound();
            }



            var OrientationRow = db.orientation_tr_mat.Find(id);

            db.orientation_tr_mat.Remove(OrientationRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Function
        [HttpDelete]

        public JsonResult delOrientation(int id)

        {
            var db = new qhsedbEntities();
            var OrientationRow = db.orientation_tr_mat.Find(id);

            db.orientation_tr_mat.Remove(OrientationRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllOrientation()
        {

            var db = new qhsedbEntities();
            var data = db.orientation_tr_mat.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetOrientationById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var OrientationId = Convert.ToInt32(id);
                var data = db.orientation_tr_mat.Find(OrientationId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetOrientationByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.orientation_tr_mat
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new Orientation()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                review_date = p.review_date,
                                work_dept = p.work_dept,
                                desc_train_ori = p.desc_train_ori,
                                notes = p.notes,
                                orient_file_1 = p.orient_file_1,
                                orient_fiile_2 = p.orient_fiile_2,
                                orient_file_3 = p.orient_file_3,
                                orient_file_4 = p.orient_file_4,
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
                    m.orientationDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
