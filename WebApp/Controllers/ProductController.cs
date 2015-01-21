using System;
using System.Collections.Generic;
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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Product/
        public ActionResult Index()
        {
            return View(db.ProductModels.ToList());
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productmodel = db.ProductModels.Find(id);
            List<CommentModel> commentlistmodel = db.CommentModels.ToList();
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



            /*
                var profileModel = db.Users.First(e => e.UserName == WebSecurity.CurrentUserName);
                 var userModel = //fetch from db.

            var pmViewModel = new ProfileUserViewModel  
                          {
                              ProfileModelObject = profileModel,
                              UserModelObject = userModel
                          };

   r                eturn View(pmViewModel);
             * */
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(CommentProductViewModel cpviewmodel)
        {
            cpviewmodel.commentObject.ApplicationUserId = User.Identity.GetUserId();
            cpviewmodel.commentObject.DateOfPublication = DateTime.Now;
            cpviewmodel.commentObject.ProductId = cpviewmodel.productObject.Id;

            db.CommentModels.Add(cpviewmodel.commentObject);

            return View(cpviewmodel);
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
