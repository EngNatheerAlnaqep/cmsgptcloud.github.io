using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cmsgptcloud.Models;
using cmsgptcloud.Controllers;
using System.Runtime.Remoting.Contexts;

namespace cmsgptcloud.Controllers
{
    public class UserController : Controller
    {
        private   smsEntities db = new smsEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.tb_user.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,username,userpassword,useremail,useraddress,userphone,usersub ,state")] tb_user tb_user)
        {
            if (ModelState.IsValid)
            {


                db.tb_user.Add(tb_user);
                db.SaveChanges();
                var rec = db.tb_user.Where(x => x.userid == tb_user.userid && x.username == tb_user.username).ToList().FirstOrDefault();
                Session["username"] = rec.username;
                Session["password"] = rec.userphone;

                string cookievalue;
                if (Request.Cookies["cookie"] != null)
                {
                    cookievalue = Request.Cookies["cookie"].Value.ToString();
                }
                else
                {
                    Response.Cookies["cookie"].Value = "cookie value";
                }
                HttpCookie cookie = new HttpCookie("WTR");
                cookie["website"] = "WebTrainingRoom";
                // This cookie will remain  for one month.
                cookie.Expires = DateTime.Now.AddMinutes(60);

                // Add it to the current web response.
                Response.Cookies.Add(cookie);
                if (Session["link"] == "Index")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return View(tb_user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,username,userpassword,uaeremail,useraddress,userphone,usersub")] tb_user tb_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_user tb_user = db.tb_user.Find(id);
            db.tb_user.Remove(tb_user);
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
        
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login([Bind(Include = "useremail,userpassword")] tb_user tb_User)
        {

            var rec = db.tb_user.Where(x => x.userpassword == tb_User.userpassword && x.useremail == tb_User.useremail).ToList()
                .FirstOrDefault();



            if (rec != null)
            {
                HttpCookie usercooki = new HttpCookie("user");
                usercooki.Value = rec.username.ToString();
                HttpCookie statcooki = new HttpCookie("state", rec.state.ToString());
                usercooki.Expires = DateTime.Now.AddDays(10);
                statcooki.Expires = DateTime.Now.AddDays(10);

                HttpContext.Response.SetCookie(usercooki);
                HttpContext.Response.SetCookie(statcooki);



                
                Session["username"] = rec.username;
                Session["password"] = rec.userpassword;
                Session["link"] = "Index";
                return RedirectToAction("Create", "tb_order");

            }
            else
            {
                ViewBag.error = "not";
                return View();


            }

        }
        
       
        public ActionResult logout()
        {
            var c = new HttpCookie("user")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            var v = new HttpCookie("state")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(v);
            Response.Cookies.Add(c);

            return RedirectToAction("login");
        }
    }
}
