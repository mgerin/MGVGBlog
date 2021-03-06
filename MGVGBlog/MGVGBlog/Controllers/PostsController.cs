﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MGVGBlog.Models;

namespace MGVGBlog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(string search)
        {
            List<Post> posts = db.Posts.Include(p => p.Author).OrderBy(p => p.Id).ToList();
            var result = new List<Post>();
            if (search != null)
            {
                result = posts.ToList().Where(p => p.Body.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                result = posts;
            }

            ViewBag.posts = result;
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            List<Post> posts = db.Posts.Include(p => p.Author).OrderBy(p => p.Id).ToList();
            ViewBag.posts = posts;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            List<Comments> coments = db.Comments.Include(c => c.Author).Where(c => c.PostId == id).OrderByDescending(c => c.Date).ToList();
            ViewBag.comments = coments;
            ViewBag.commentsCount = coments.Count;
            List<Reply> replies =
                db.Replies.Include(r => r.Author).Where(r => r.PostId == id).OrderBy(r => r.Date).ToList();


            ViewBag.replies = replies;
           

            Session["id"] = post.Id;
            

            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }
        [Authorize]
        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            Post post = db.Posts.Find(id);
            List<Post> wqer = db.Posts.Include(p => p.Author).OrderBy(p => p.Id).ToList();
            ViewBag.postAuthor = post.Author.UserName;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ViewBag.postAuthor != User.Identity.Name && User.IsInRole("Member"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
