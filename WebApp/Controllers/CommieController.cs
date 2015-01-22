using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class CommieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Commie/
        public ActionResult Index()
        {
            var commentmodels = db.CommentModels.Include(c => c.User).Include(c => c.Product);
            return View(commentmodels.ToList());
        }

        // GET: /Commie/Details/5
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<CommentModel> commentmodels = db.CommentModels.Where(m => m.ProductId == id).ToList();
            if (commentmodels == null)
            {
                return HttpNotFound();
            }
            return View(commentmodels);
        }

        // GET: /Commie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.CommentModels.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // GET: /Commie/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.ProductId = new SelectList(db.ProductModels, "Id", "Path");
            return View();
        }

        // POST: /Commie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create([Bind(Include="Id,Content,DateOfPublication,ProductId,ApplicationUserId")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.CommentModels.Add(commentmodel);
                db.SaveChanges();
            }


        }

        // GET: /Commie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.CommentModels.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", commentmodel.ApplicationUserId);
            ViewBag.ProductId = new SelectList(db.ProductModels, "Id", "Path", commentmodel.ProductId);
            return View(commentmodel);
        }

        // POST: /Commie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Content,DateOfPublication,ProductId,ApplicationUserId")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserName", commentmodel.ApplicationUserId);
            ViewBag.ProductId = new SelectList(db.ProductModels, "Id", "Path", commentmodel.ProductId);
            return View(commentmodel);
        }

        // GET: /Commie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.CommentModels.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // POST: /Commie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentmodel = db.CommentModels.Find(id);
            db.CommentModels.Remove(commentmodel);
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
