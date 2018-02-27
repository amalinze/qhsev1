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
    public class WitnessStmtController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.witness_statement.ToList());
        }
        [HttpPost]
        public JsonResult AddWitnessStmt(witness_statement WitnessStmtObj)
        {
            try
            {
                WitnessStmtObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.witness_statement.Add(WitnessStmtObj);
                db.SaveChanges();




                return Json("Added New WitnessStmt Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(witness_statement witness_statement)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.witness_statement.Add(witness_statement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(witness_statement);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            witness_statement witness_statement = db.witness_statement.Find(id);
            if (witness_statement == null)
            {
                return HttpNotFound();
            }
            return View(witness_statement);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(witness_statement WitnessStmtObj)
        {
            try
            {
                WitnessStmtObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    WitnessStmtObj.id = WitnessStmtObj.id;
                    db.Entry(WitnessStmtObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited WitnessStmt System", JsonRequestBehavior.AllowGet);


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
            witness_statement witness_statement = db.witness_statement.Find(id);
            if (witness_statement == null)
            {
                return HttpNotFound();
            }



            var WitnessStmtRow = db.witness_statement.Find(id);

            db.witness_statement.Remove(WitnessStmtRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllWitnessStmt()
        {

            var db = new qhsedbEntities();
            var data = db.witness_statement.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetWitnessStmtById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var WitnessStmtId = Convert.ToInt32(id);
                var data = db.witness_statement.Find(WitnessStmtId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetWitnessStmtByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.witness_statement
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new WitnessStmt()
                            {

                                id = p.id,
                                incident_ref_no = p.incident_ref_no,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                project_name = p.project_name,
                                work_area_dept = p.work_area_dept,
                                position = p.position,
                                name_TP = p.name_TP,
                                phone = p.phone,
                                email_addr = p.email_addr,
                                addrress = p.addrress,
                                state_province = p.state_province,
                                city = p.city,
                                employer = p.employer,
                                first_wit_name = p.first_wit_name,
                                first_wit_phone = p.first_wit_phone,
                                first_wit_comp = p.first_wit_comp,
                                second_wit_name = p.second_wit_name,
                                second_wit_phone = p.second_wit_phone,
                                second_wit_comp = p.second_wit_comp,
                                third_wit_name = p.third_wit_name,
                                third_wit_phone = p.third_wit_phone,
                                third_wit_comp = p.third_wit_comp,
                                incident_desc = p.incident_desc,
                                statement = p.statement,
                                signature = p.signature,
                                sig_date = p.sig_date,
                                signed_witness_stmt = p.signed_witness_stmt,
                                attachment_1 = p.attachment_1,
                                attachment_2 = p.attachment_2,
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
                    m.witnessStmtDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
