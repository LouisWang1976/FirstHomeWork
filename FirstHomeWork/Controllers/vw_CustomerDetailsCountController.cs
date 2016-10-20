using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstHomeWork.Models;

namespace FirstHomeWork.Controllers
{
    public class vw_CustomerDetailsCountController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: vw_CustomerDetailsCount
        public ActionResult Index()
        {
            return View(db.vw_CustomerDetailsCount.ToList());
        }

        // GET: vw_CustomerDetailsCount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vw_CustomerDetailsCount vw_CustomerDetailsCount = db.vw_CustomerDetailsCount.Find(id);
            if (vw_CustomerDetailsCount == null)
            {
                return HttpNotFound();
            }
            return View(vw_CustomerDetailsCount);
        }

        // GET: vw_CustomerDetailsCount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vw_CustomerDetailsCount/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Customer,CountAccount,CountContact")] vw_CustomerDetailsCount vw_CustomerDetailsCount)
        {
            if (ModelState.IsValid)
            {
                db.vw_CustomerDetailsCount.Add(vw_CustomerDetailsCount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vw_CustomerDetailsCount);
        }

        // GET: vw_CustomerDetailsCount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vw_CustomerDetailsCount vw_CustomerDetailsCount = db.vw_CustomerDetailsCount.Find(id);
            if (vw_CustomerDetailsCount == null)
            {
                return HttpNotFound();
            }
            return View(vw_CustomerDetailsCount);
        }

        // POST: vw_CustomerDetailsCount/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Customer,CountAccount,CountContact")] vw_CustomerDetailsCount vw_CustomerDetailsCount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vw_CustomerDetailsCount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vw_CustomerDetailsCount);
        }

        // GET: vw_CustomerDetailsCount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vw_CustomerDetailsCount vw_CustomerDetailsCount = db.vw_CustomerDetailsCount.Find(id);
            if (vw_CustomerDetailsCount == null)
            {
                return HttpNotFound();
            }
            return View(vw_CustomerDetailsCount);
        }

        // POST: vw_CustomerDetailsCount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vw_CustomerDetailsCount vw_CustomerDetailsCount = db.vw_CustomerDetailsCount.Find(id);
            db.vw_CustomerDetailsCount.Remove(vw_CustomerDetailsCount);
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
