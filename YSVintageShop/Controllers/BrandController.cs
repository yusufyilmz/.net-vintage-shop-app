using Newtonsoft.Json;
using System.Collections.Generic;
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
    public class BrandController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BrandModel brand = db.Brands.Include(s => s.Files).SingleOrDefault(s => s.BrandId == id);

            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: brands/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandId,Title,Description,Slug,Status")] BrandModel brand, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var avatar = FileUploader.Upload(upload);
                    if (avatar != null)
                    {
                        brand.Files = new List<FileModel> { avatar };
                    }
                    else
                    {
                        ModelState.AddModelError("", "Image file null or could not be uploaded");
                    }

                    db.Brands.Add(brand);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(brand);
        }


        // GET: brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandModel brand = db.Brands.Include(s => s.Files).SingleOrDefault(s => s.BrandId == id);

            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: brands/Edit/5
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
            BrandModel brand = db.Brands.Include(s => s.Files).SingleOrDefault(s => s.BrandId == id);
            if (TryUpdateModel(brand, "", new string[] { "BrandId", "Title", "Description", "Slug", "Status" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (brand.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(brand.Files.First(f => f.FileType == FileType.Avatar));
                        }

                        var avatar = FileUploader.Upload(upload);
                        if (avatar != null) brand.Files = new List<FileModel> { avatar };
                    }
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(brand);
        }


        // GET: brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandModel brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BrandModel brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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
