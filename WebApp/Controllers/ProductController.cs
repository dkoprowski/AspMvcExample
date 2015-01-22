using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Product/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.ProductModels.ToList());
        }


        // GET: /Product/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productmodel = db.ProductModels.Find(id);
            List<CommentModel> commentlistmodel = db.CommentModels.Where(m => m.ProductId == id).ToList();
            CommentModel commentmodel = new CommentModel();
            if (productmodel == null)
            {
                return HttpNotFound();
            }

            var cpViewModel = new CommentProductViewModel
            {
                commentListObject = commentlistmodel,
                productObject = productmodel,
                commentObject = commentmodel
            };

            return View(cpViewModel);
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Details(CommentProductViewModel cpviewmodel)
        {
            //cpviewmodel.commentObject.ApplicationUserId = User.Identity.GetUserId();
            cpviewmodel.commentObject.DateOfPublication = DateTime.Now;
            cpviewmodel.commentObject.ProductId = cpviewmodel.productObject.Id;

            db.CommentModels.Add(cpviewmodel.commentObject);
            db.SaveChanges();

            Response.Redirect(cpviewmodel.productObject.Id.ToString());
           // Details(cpviewmodel.productObject.Id);

        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Path,ReleaseDate,Title,Description")] ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                db.ProductModels.Add(productmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productmodel);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productmodel = db.ProductModels.Find(id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Path,ReleaseDate,Title,Description")] ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productmodel);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productmodel = db.ProductModels.Find(id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productmodel = db.ProductModels.Find(id);
            db.ProductModels.Remove(productmodel);
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
