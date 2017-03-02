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
    public class MainHeadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MainHeadModels
        public ActionResult Index()
        {
            return View(db.MainHeads.ToList());
        }

        // GET: MainHeadModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainHeadModel mainHeadModel = db.MainHeads.Include(s => s.Files).SingleOrDefault(s => s.MainHeadId == id);

            if (mainHeadModel == null)
            {
                return HttpNotFound();
            }
            return View(mainHeadModel);
        }

        // GET: MainHeadModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainHeadModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MainHeadId,Title,Description,Slug,Status")] MainHeadModel mainHeadModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var avatar = FileUploader.Upload(upload);
                if (avatar != null) mainHeadModel.Files = new List<FileModel> { avatar };
                db.MainHeads.Add(mainHeadModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mainHeadModel);
        }

        // GET: MainHeadModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainHeadModel mainHeadModel = db.MainHeads.Include(s => s.Files).SingleOrDefault(s => s.MainHeadId == id);

            if (mainHeadModel == null)
            {
                return HttpNotFound();
            }
            return View(mainHeadModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainHeadModel meainHead = db.MainHeads.Include(s => s.Files).SingleOrDefault(s => s.MainHeadId == id);
            if (TryUpdateModel(meainHead, "", new string[] { "MainHeadId", "Title", "Description", "Slug", "Status" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (meainHead.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(meainHead.Files.First(f => f.FileType == FileType.Avatar));
                        }

                        var avatar = FileUploader.Upload(upload);
                        if (avatar != null) meainHead.Files = new List<FileModel> { avatar };
                    }
                    db.Entry(meainHead).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(meainHead);
        }



        // POST: MainHeadModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MainHeadId,Title,Description,Slug,Status")] MainHeadModel mainHeadModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(mainHeadModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(mainHeadModel);
        //}

        // GET: MainHeadModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainHeadModel mainHeadModel = db.MainHeads.Find(id);
            if (mainHeadModel == null)
            {
                return HttpNotFound();
            }
            return View(mainHeadModel);
        }

        // POST: MainHeadModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainHeadModel mainHeadModel = db.MainHeads.Find(id);
            db.MainHeads.Remove(mainHeadModel);
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
