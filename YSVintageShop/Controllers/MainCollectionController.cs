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
    public class MainCollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MainCollectionModels
        public ActionResult Index()
        {
            return View(db.MainCollections.ToList());
        }

        // GET: MainCollectionModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCollectionModel collection = db.MainCollections.Include(s => s.Files).SingleOrDefault(s => s.MainCollectionId == id);

            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: MainCollectionModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainCollectionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MainCollectionId,Title,Description,Slug,Status")] MainCollectionModel mainCollectionModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var avatar = FileUploader.Upload(upload);
                if (avatar != null) mainCollectionModel.Files = new List<FileModel> { avatar };
                db.MainCollections.Add(mainCollectionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mainCollectionModel);
        }

        // GET: MainCollectionModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCollectionModel mainCollectionModel = db.MainCollections.Include(s => s.Files).SingleOrDefault(s => s.MainCollectionId == id);
            if (mainCollectionModel == null)
            {
                return HttpNotFound();
            }
            return View(mainCollectionModel);
        }

        // POST: MainCollectionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MainCollectionId,Title,Description,Slug,Status")] MainCollectionModel mainCollectionModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(mainCollectionModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(mainCollectionModel);
        //}




        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCollectionModel collection = db.MainCollections.Include(s => s.Files).SingleOrDefault(s => s.MainCollectionId == id);
            if (TryUpdateModel(collection, "", new string[] { "MainCollectionId", "Title", "Description", "Slug", "Status" }))
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
            return View(collection);
        }


        // GET: MainCollectionModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCollectionModel mainCollectionModel = db.MainCollections.Find(id);
            if (mainCollectionModel == null)
            {
                return HttpNotFound();
            }
            return View(mainCollectionModel);
        }

        // POST: MainCollectionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainCollectionModel mainCollectionModel = db.MainCollections.Find(id);
            db.MainCollections.Remove(mainCollectionModel);
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
