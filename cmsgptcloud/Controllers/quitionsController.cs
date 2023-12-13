using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmsgptcloud.Models;
using System.IO;
using cmsgptcloud.Controllers;


namespace cmsgptcloud.Controllers
{
    public class quitionsController : Controller
    {
        private smsEntities db = new smsEntities();

        // GET: quitions
        public ActionResult Index()
        {
            return View(db.quitions.ToList());
        }

        // GET: quitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quitions quition = db.quitions.Find(id);
            if (quition == null)
            {
                return HttpNotFound();
            }
            return View(quition);
        }

        // GET: quitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: quitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QId,mname,Qquition,Qanuser,Qcode,Qnote")] quitions quition)
        {
            if (ModelState.IsValid)
            {
                db.quitions.Add(quition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quition);
        }

        // GET: quitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quitions quition = db.quitions.Find(id);
            if (quition == null)
            {
                return HttpNotFound();
            }
            return View(quition);
        }

        // POST: quitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QId,mname,Qquition,Qanuser,Qcode,Qnote")] quitions quition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quition);
        }

        // GET: quitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quitions quition = db.quitions.Find(id);
            if (quition == null)
            {
                return HttpNotFound();
            }
            return View(quition);
        }

        // POST: quitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            quitions quition = db.quitions.Find(id);
            db.quitions.Remove(quition);
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

        
       
        public ActionResult Display(String Search)
        {
            smsEntities db = new smsEntities();

            return View( db.quitions.Where(a => a.Qquition.Contains(Search) || a.Qanuser.Contains(Search) || Search==null).ToList());
           
            
            
           
        }

        public ActionResult Displayone(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quitions quition = db.quitions.Find(id);
            if (quition == null)
            {
                return HttpNotFound();
            }
            return View(quition);
        }
       
        public ActionResult Downloadfile(string Filename)
        {
            string path = Server.MapPath("~/orderfile") + Filename;
            if (path != null)
            {


                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", Filename);
            }
            else
            {
                return View("Dispalyone");
            }
        }
    }
}
