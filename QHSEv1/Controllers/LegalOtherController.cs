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
    public class LegalOtherController : Controller
    {
        
            public ActionResult Index()
            {
                var db = new qhsedbEntities();
                return View(db.legal_other_requierments.ToList());
            }

            // GET: /Create
            public ActionResult Create()
            {
                return View();
            }


            [HttpPost]
            public ActionResult Create(legal_other_requierments legal_other_requierments)
            {
                if (ModelState.IsValid)
                {
                    var db = new qhsedbEntities();
                    db.legal_other_requierments.Add(legal_other_requierments);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(legal_other_requierments);
            }


            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var db = new qhsedbEntities();
                legal_other_requierments legal_other_requierments = db.legal_other_requierments.Find(id);
                if (legal_other_requierments == null)
                {
                    return HttpNotFound();
                }
                return View(legal_other_requierments);
            }
            //Edit Function
            [HttpPost]
            public JsonResult Edit(legal_other_requierments legal_other_requierments)
            {
                try
                {

                    var db = new qhsedbEntities();
                    if (ModelState.IsValid)
                    {
                        legal_other_requierments.id = legal_other_requierments.id;
                        db.Entry(legal_other_requierments).State = EntityState.Modified;

                        // loanRequest.LoanDate = loanRequest.LoanDate;
                        db.SaveChanges();

                    }



                    return Json("Edited Legal  System", JsonRequestBehavior.AllowGet);


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
                legal_other_requierments legal_other_requierments = db.legal_other_requierments.Find(id);
                if (legal_other_requierments == null)
                {
                    return HttpNotFound();
                }



                var legal_other_requiermentsRow = db.legal_other_requierments.Find(id);

                db.legal_other_requierments.Remove(legal_other_requiermentsRow);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //get record by parameter
            public JsonResult GetLegalOtherById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var LegalOtherId = Convert.ToInt32(id);
                var data = db.legal_other_requierments.Find(LegalOtherId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetLegalOtherByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.legal_other_requierments
                                .OrderBy(c => c.legal_other_requierments_date)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new LegalOther()
                            {

                                id = p.id,
                                legal_other_requierments_date = p.legal_other_requierments_date,
                                description_of_requirements = p.description_of_requirements,
                                notes = p.notes,
                                url = p.url,
                                title = p.title,
                                attach_requirements = p.attach_requirements,
                                userID = p.userID,

                            }
                                  );
                if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
                {
                    DateTime fromdate = Convert.ToDateTime(fromDate);
                    DateTime todate = Convert.ToDateTime(toDate);
                    data = data.Where(p => (p.legal_other_requierments_date > fromdate && p.legal_other_requierments_date < todate));
                }
                else
                {
                    data = data.Where(p => (p.legal_other_requierments_date < DateTime.Now));



                }

                var dataResult = data.ToList();

                dataResult.ForEach(m =>
                {
                    m.legalOtherDateStr = Convert.ToString(Convert.ToDateTime(m.legal_other_requierments_date).Month + "/" + Convert.ToDateTime(m.legal_other_requierments_date).Day + "/" + Convert.ToDateTime(m.legal_other_requierments_date).Year);


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
