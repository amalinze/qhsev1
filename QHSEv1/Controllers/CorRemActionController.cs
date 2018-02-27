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
    public class CorRemActionController: Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.cor_rem_action.ToList());
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(cor_rem_action com_rem_action)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.cor_rem_action.Add(com_rem_action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(com_rem_action);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
           cor_rem_action CorRemAction = db.cor_rem_action.Find(id);
            if (CorRemAction == null)
            {
                return HttpNotFound();
            }
            return View(CorRemAction);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(cor_rem_action CorRemAction)
        {
            try
            {

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    CorRemAction.id = CorRemAction.id;
                    db.Entry(CorRemAction).State = EntityState.Modified;

                  
                    db.SaveChanges();

                }



                return Json("Edited Corrective Remedial Action System", JsonRequestBehavior.AllowGet);


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
           cor_rem_action CorRemAction = db.cor_rem_action.Find(id);
            if (CorRemAction == null)
            {
                return HttpNotFound();
            }



            var CorRemActionRow = db.cor_rem_action.Find(id);

            db.cor_rem_action.Remove(CorRemActionRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //get record by parameter
        public JsonResult GetCorRemActionById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var CorRemActionId = Convert.ToInt32(id);
                var data = db.cor_rem_action.Find(CorRemActionId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetCorRemActionByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.cor_rem_action
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new CorRemAction()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                action_type = p.action_type,
                                action_name = p.action_name,
                                action_implimented = p.action_implimented,
                                completion_date = p.completion_date,
                                action_com_by = p.action_com_by,
                                results_of_actions = p.results_of_actions,
                                staatus_of_action = p.staatus_of_action,
                                attachment = p.attachment,
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
                    m.corRemActionDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
