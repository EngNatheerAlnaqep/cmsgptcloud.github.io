using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmsgptcloud.Models;


namespace cmsgptcloud.Controllers
{
    public class tb_orderController : Controller
    {
        private smsEntities db = new smsEntities();

        // GET: tb_order
        public ActionResult Index()
        {
            return View(db.tb_order.ToList());
        }

        // GET: tb_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
            if (tb_order == null)
            {
                return HttpNotFound();
            }
            return View(tb_order);
        }

        // GET: tb_order/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: tb_order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orid,username,ormesg,orpictire,ortime")] tb_order tb_order ,HttpPostedFileBase imgfile,HttpPostedFileBase orderfile )
        {
            if (ModelState.IsValid)
            {
                
                
                    string mpath = "";
                    string fpath = "";
                    
                    if (orderfile.FileName.Length > 0)
                    {
                        fpath = "~/orderfile/" + Path.GetFileName(orderfile.FileName);
                        orderfile.SaveAs(Server.MapPath(fpath));
                 }
                 else if (imgfile.FileName.Length > 0)
                        {
                        mpath = "~/images/" + Path.GetFileName(imgfile.FileName);
                      imgfile.SaveAs(Server.MapPath(mpath));
                }
                else

                {
                     mpath = "no";
                     fpath = "no";
                }
                tb_order.orpictire = mpath;
                tb_order.orfile = fpath;
                db.tb_order.Add(tb_order);
                db.SaveChanges();
               Session["addorder"] = "yes";



                return RedirectToAction( "Index","Home");
            }

            return View(tb_order);
        }

        // GET: tb_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
           

            if (tb_order == null)
            {
                return HttpNotFound();
            }
            return View(tb_order);
        }

        // POST: tb_order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orid,username,ormesg,orfile,orpictire,ortime")] tb_order tb_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_order);
        }

        // GET: tb_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
            if (tb_order == null)
            {
                return HttpNotFound();
            }
            return View(tb_order);
        }

        // POST: tb_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_order tb_order = db.tb_order.Find(id);
            db.tb_order.Remove(tb_order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

