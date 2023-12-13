using cmsgptcloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace cmsgptcloud.Controllers
{
    public class HomeController : Controller
    {
        smsEntities db = new smsEntities();
        // GET: Home
        public ActionResult Index()
        {
           
            var name = Session["username"];
            var pass = Session["password"];
            db.SaveChanges();
            string d = "true";
            Session["State"] = d;
            var rec = db.tb_user.Where(x =>x.username == name && x.userpassword == pass && x.state == false).ToList();
            if (rec.Count()<0)
            {
                d = "False";
               
                Session["State"] =d;
                return View();
            }
            else { return View(); }
           
        }
        public ActionResult Display()
        {
            return View(db.quitions.ToList());
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

       public ActionResult Javaproject()
        {
            return View();
        }
        
        

        
    }
}