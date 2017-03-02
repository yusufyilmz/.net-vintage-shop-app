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

namespace YSVintageShop.Controllers
{
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collection
        public ActionResult Index()
        {
            var collectionModels = db.Collections.Include(s => s.MainCollection);

            return View(collectionModels.ToList());
            //return View(db.Collection.ToList());
        }

        // GET: Collection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionModel collection = db.Collections.Include(s => s.Files).Include(s => s.MainCollection).SingleOrDefault(s => s.CollectionId == id);

            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collection/Create
        public ActionResult Create()
        {
            ViewBag.MainCollectionId = new SelectList(db.MainCollections, "MainCollectionId", "Title");

            return View();
        }

        // POST: Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionId,Title,Description,Slug,Status, MainCollectionId")] CollectionModel collection, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var avatar = FileUploader.Upload(upload);
                if (avatar != null) collection.Files = new List<FileModel> { avatar };


                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MainCollectionId = new SelectList(db.MainCollections, "MainCollectionId", "Title", collection.MainCollectionId);
            return View(collection);
        }

        // GET: Collection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionModel collection = db.Collections.Include(s => s.Files).SingleOrDefault(s => s.CollectionId == id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            ViewBag.MainCollectionId = new SelectList(db.MainCollections, "MainCollectionId", "Title", collection.MainCollectionId);
            return View(collection);
        }

        // POST: Collection/Edit/5
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
            CollectionModel collection = db.Collections.Include(s => s.Files).SingleOrDefault(s => s.CollectionId == id);
            if (TryUpdateModel(collection, "", new string[] { "CollectionId", "Title", "Description", "Slug", "Status", "MainCollectionId" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (collection.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(collection.Files.First(f => f.FileType == FileType.Avatar));
                        }

                        var avatar = FileUploader.Upload(upload);
                        if (avatar != null) collection.Files = new List<FileModel> { avatar };
                    }
                    db.Entry(collection).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.CollectionId = id;

            return View(collection);
        }


        // GET: Collection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionModel collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollectionModel collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
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
