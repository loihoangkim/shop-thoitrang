using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private ShopThoiTrangDBContext db = new ShopThoiTrangDBContext();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var list = db.Categories.Where(m => m.Status != 0)
                .OrderByDescending(m => m.Created_At)
                .ToList();
            return View("Index", list);
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Slug,ParentId,Orders,Metakey,Metadesc,Created_By,Created_At,Updated_By,Updated_At,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Created_At = DateTime.Now;
                category.Created_By = 1;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,ParentId,Orders,Metakey,Metadesc,Created_By,Created_At,Updated_By,Updated_At,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Updated_At = DateTime.Now;
                category.Updated_By = 1;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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

        // thay đổi trạng thái
        public ActionResult Status(int id)
        {
            Category category = db.Categories.Find(id);
            int status = (category.Status == 1) ? 2 : 1;
            category.Status = status;
            category.Updated_By = 1;
            category.Updated_At = DateTime.Now;
            // cập nhật
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // thay đổi trạng thái về 0 ~ disable
        public ActionResult MoveToTrash(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 0;
            category.Updated_By = 1;
            category.Updated_At = DateTime.Now;
            // cập nhật
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Categories.Where(m => m.Status == 0)
                .OrderByDescending(m => m.Updated_At)
                .ToList();
            return View(list);
        }

        public ActionResult ReTrash(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 1;
            category.Updated_By = 1;
            category.Updated_At = DateTime.Now;
            // cập nhật
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
