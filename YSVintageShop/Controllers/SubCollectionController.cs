//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using YSVintageShop.Models;
//using YSVintageShop.Utils;

//namespace YSVintageShop.Controllers
//{
//    public class SubCollectionController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: SubCollectionModels
//        public ActionResult Index()
//        {
//            var subCollectionModels = db.SubCollections.Include(s => s.Collection);

//            return View(subCollectionModels.ToList());
//        }

//        // GET: SubCollectionModels/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MainCollectionModel subCollectionModel = db.SubCollections.Include(s => s.Files).SingleOrDefault(s => s.MainCollectionId == id);

//            if (subCollectionModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(subCollectionModel);
//        }

//        // GET: SubCollectionModels/Create
//        public ActionResult Create()
//        {
//            ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title");
//            return View();
//        }

//        // POST: SubCollectionModels/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "SubCollectionId,Title,Description,Slug,Status,CollectionId")] MainCollectionModel subCollectionModel, HttpPostedFileBase upload)
//        {
//            if (ModelState.IsValid)
//            {
//                var avatar = FileUploader.Upload(upload);
//                if (avatar != null) subCollectionModel.Files = new List<FileModel> { avatar };
//                db.SubCollections.Add(subCollectionModel);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title", subCollectionModel.CollectionId);
//            return View(subCollectionModel);
//        }

//        // GET: SubCollectionModels/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MainCollectionModel subCollectionModel = db.SubCollections.Include(s => s.Files).SingleOrDefault(s => s.MainCollectionId == id);

//            if (subCollectionModel == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title", subCollectionModel.CollectionId);
//            return View(subCollectionModel);
//        }

//        // POST: SubCollectionModels/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult Edit([Bind(Include = "SubCollectionId,Title,Description,Slug,Status,CollectionId")] SubCollectionModel subCollectionModel)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.Entry(subCollectionModel).State = EntityState.Modified;
//        //        db.SaveChanges();
//        //        return RedirectToAction("Index");
//        //    }
//        //    ViewBag.CollectionId = new SelectList(db.Collections, "CollectionId", "Title", subCollectionModel.CollectionId);
//        //    return View(subCollectionModel);
//        //}


//        [HttpPost, ActionName("Edit")]
//        [ValidateAntiForgeryToken]
//        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MainCollectionModel subCollection = db.SubCollections.Include(s => s.Files).SingleOrDefault(s => s.MainCollectionId == id);
//            if (TryUpdateModel(subCollection, "", new string[] { "SubCollectionId", "Title", "Description", "Slug", "Status", "CollectionId" }))
//            {
//                try
//                {
//                    if (upload != null && upload.ContentLength > 0)
//                    {
//                        if (subCollection.Files.Any(f => f.FileType == FileType.Avatar))
//                        {
//                            db.Files.Remove(subCollection.Files.First(f => f.FileType == FileType.Avatar));
//                        }

//                        var avatar = FileUploader.Upload(upload);
//                        if (avatar != null) subCollection.Files = new List<FileModel> { avatar };
//                    }
//                    db.Entry(subCollection).State = EntityState.Modified;
//                    db.SaveChanges();

//                    return RedirectToAction("Index");
//                }
//                catch (RetryLimitExceededException /* dex */)
//                {
//                    //Log the error (uncomment dex variable name and add a line here to write a log.
//                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//                }
//            }

//            ViewBag.CollectionId = id;
//            return View(subCollection);
//        }


//        // GET: SubCollectionModels/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MainCollectionModel subCollectionModel = db.SubCollections.Find(id);
//            if (subCollectionModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(subCollectionModel);
//        }

//        // POST: SubCollectionModels/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            MainCollectionModel subCollectionModel = db.SubCollections.Find(id);
//            db.SubCollections.Remove(subCollectionModel);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
