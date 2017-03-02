using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YSVintageShop.Models;
using YSVintageShop.Utils;

namespace AngularJSWebApiEmpty.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: products
        public ActionResult Index()
        {
            var products = db.Products.Include(y => y.Brand).Include(y => y.Collection);
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Products.Include(s => s.Files).SingleOrDefault(s => s.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Title");
            ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Title,Description,Slug,Status,Price,Stock,BrandId,CollectionId")] ProductModel product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var avatar = FileUploader.Upload(upload);
                if (avatar != null) product.Files = new List<FileModel> { avatar };

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Title", product.BrandId);
            ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title", product.CollectionId);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Products.Include(s => s.Files).SingleOrDefault(s => s.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Title", product.BrandId);
            ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title", product.CollectionId);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Products.Include(s => s.Files).SingleOrDefault(s => s.ProductId == id);
            if (TryUpdateModel(product, "", new string[] { "ProductId", "Title", "Description", "Slug", "Status", "Price", "Stock", "BrandId", "CollectionId" }))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            if (product.Files.Any(f => f.FileType == FileType.Avatar))
                            {
                                db.Files.Remove(product.Files.First(f => f.FileType == FileType.Avatar));
                            }

                            var avatar = FileUploader.Upload(upload);
                            if (avatar != null) product.Files = new List<FileModel> { avatar };
                        }
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Title", product.BrandId);
                    ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title", product.CollectionId);
                    return View(product);
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel product = db.Products.Find(id);
            db.Products.Remove(product);
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
