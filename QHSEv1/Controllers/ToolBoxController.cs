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
    public class ToolBoxController : Controller
    {
        public ActionResult Index()
        {
            var db = new qhsedbEntities();
            return View(db.tool_box_topic.ToList());
        }
        [HttpPost]
        public JsonResult AddToolBox(tool_box_topic ToolBoxObj)
        {
            try
            {
                ToolBoxObj.date_of_entry = DateTime.Now;
                var db = new qhsedbEntities();
                db.tool_box_topic.Add(ToolBoxObj);
                db.SaveChanges();




                return Json("Added New ToolBox Entry Successfully", JsonRequestBehavior.AllowGet);


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
        public ActionResult Create(tool_box_topic tool_box_topic)
        {
            if (ModelState.IsValid)
            {
                var db = new qhsedbEntities();
                db.tool_box_topic.Add(tool_box_topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tool_box_topic);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db = new qhsedbEntities();
            tool_box_topic tool_box_topic = db.tool_box_topic.Find(id);
            if (tool_box_topic == null)
            {
                return HttpNotFound();
            }
            return View(tool_box_topic);
        }
        //Edit Function
        [HttpPost]
        public JsonResult Edit(tool_box_topic ToolBoxObj)
        {
            try
            {
                ToolBoxObj.date_of_entry = DateTime.Now;

                var db = new qhsedbEntities();
                if (ModelState.IsValid)
                {
                    ToolBoxObj.id = ToolBoxObj.id;
                    db.Entry(ToolBoxObj).State = EntityState.Modified;

                    // loanRequest.LoanDate = loanRequest.LoanDate;
                    db.SaveChanges();

                }



                return Json("Edited ToolBox System", JsonRequestBehavior.AllowGet);


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
            tool_box_topic tool_box_topic = db.tool_box_topic.Find(id);
            if (tool_box_topic == null)
            {
                return HttpNotFound();
            }



            var ToolBoxRow = db.tool_box_topic.Find(id);

            db.tool_box_topic.Remove(ToolBoxRow);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Delete Function
        [HttpDelete]

        public JsonResult delToolBox(int id)

        {
            var db = new qhsedbEntities();
            var ToolBoxRow = db.tool_box_topic.Find(id);

            db.tool_box_topic.Remove(ToolBoxRow);

            db.SaveChanges();

            return Json("Deleted Record ", JsonRequestBehavior.AllowGet);

        }
        //Geta all records function
        public JsonResult GetAllToolBox()
        {

            var db = new qhsedbEntities();
            var data = db.tool_box_topic.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //get record by parameter
        public JsonResult GetToolBoxById(string id)
        {

            try
            {
                var db = new qhsedbEntities();
                var ToolBoxId = Convert.ToInt32(id);
                var data = db.tool_box_topic.Find(ToolBoxId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //Get records for reporting purposes 
        public JsonResult GetToolBoxByQueryReport(string id, string fromDate, string toDate)
        {

            try
            {
                var db = new qhsedbEntities();
                var data = (from
                                     p in db.tool_box_topic
                                .OrderBy(c => c.date_of_entry)
                                //join c in db.tblCustomers on p.EntityNum equals c.AccountNo



                            select new ToolBox()
                            {

                                id = p.id,
                                date_of_entry = p.date_of_entry,
                                topic_desc = p.topic_desc,
                                project_name = p.project_name,
                                work_dept = p.work_dept,
                                topic = p.topic,
                                question_concern = p.question_concern,
                                developer = p.developer,
                                supervisor = p.supervisor,
                                attach_tool = p.attach_tool,
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
                    m.toolBoxDateStr = Convert.ToString(Convert.ToDateTime(m.date_of_entry).Month + "/" + Convert.ToDateTime(m.date_of_entry).Day + "/" + Convert.ToDateTime(m.date_of_entry).Year);


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
