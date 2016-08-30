using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MGVGBlog.Models;
using System.Data.Entity;

namespace MGVGBlog.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [RequireHttps]
        public ActionResult Index()
        {
            List<Post> postsThree = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(3).ToList();
            ViewBag.lastPosts = postsThree;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}