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
    public class HazardSubController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.hazard_sub_emp_exp.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(hazard_sub_emp_exp hazard_sub_emp_exp)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.hazard_sub_emp_exp.Add(hazard_sub_emp_exp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hazard_sub_emp_exp);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            hazard_sub_emp_exp hazard_sub_emp_exp = db.hazard_sub_emp_exp.Find(id);
            if (hazard_sub_emp_exp == null)
            {
                return HttpNotFound();
            }
            return View(hazard_sub_emp_exp);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(hazard_sub_emp_exp hazard_sub_emp_exp)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    hazard_sub_emp_exp.id = hazard_sub_emp_exp.id;
                    db.Entry(hazard_sub_emp_exp).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited Hazard Sub System", JsonRequestBehavior.AllowGet);


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
            hazard_sub_emp_exp hazard_sub_emp_exp = db.hazard_sub_emp_exp.Find(id);
            if (hazard_sub_emp_exp == null)
            {
                return HttpNotFound();
            }



            var hazard_sub_emp_expRow = db.hazard_sub_emp_exp.Find(id);

            db.hazard_sub_emp_exp.Remove(hazard_sub_emp_expRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Geta all records function
        public JsonResult GetAllHazardSub()
        {

            var db = new qhsedbEntities();
            var data = db.hazard_sub_emp_exp.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetHazardSubById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var HazardSubId = Convert.ToInt32(id);
                var data = db.hazard_sub_emp_exp.Find(HazardSubId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetHazardSubByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.hazard_sub_emp_exp
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new HazardSub()
                            {

                                id = p.id,
                                incident_ref_no = p.incident_ref_no,
                                date_of_entry = p.date_of_entry,
                                name = p.name,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                position = p.position,
                                dt_of_exposure = p.dt_of_exposure,
                                loc_of_exposure = p.loc_of_exposure,
                                chemical_name = p.chemical_name,
                                chemical_abstract = p.chemical_abstract,
                                trad_name_chem = p.trad_name_chem,
                                type_of_exposure = p.type_of_exposure,
                                if_contact_WPBI = p.if_contact_WPBI,
                                how_did_expo_occur = p.how_did_expo_occur,
                                was_PPE_avail = p.was_PPE_avail,
                                was_PPE_used = p.was_PPE_used,
                                if_PPE = p.if_PPE,
                                was_PTI = p.was_PTI,
                                were_any_symp = p.were_any_symp,
                                if_so_desc = p.if_so_desc,
                                severity_of_exposure = p.severity_of_exposure,
                                desc_sev_of_exposure = p.desc_sev_of_exposure,
                                long_time_from_work = p.long_time_from_work,
                                est_lost_time = p.est_lost_time,
                                list_sug = p.list_sug,
                                supervisor = p.supervisor,
                                MSDS = p.MSDS,
                                physicians_report = p.physicians_report,
                                invest_report = p.invest_report,
                                other_attachment = p.other_attachment,
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
                    m.hazardSubDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
