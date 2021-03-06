﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstHomeWork.Models;
using PagedList;
using FirstHomeWork.Classes;

namespace FirstHomeWork.Controllers
{
    public class ContactPersonController : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        private CustomerEntities db = new CustomerEntities();
        private int pageSize = 10;
        // GET: ContactPerson
        public ActionResult Index(string search1, string search2,int page=1)
        {
            int currentPage = page < 1 ? 1 : page;
            var data = repo.GetAllDataOrderById(search1, search2, page);
            var result = data.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        // GET: ContactPerson/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 =repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: ContactPerson/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: ContactPerson/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsDeleted")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: ContactPerson/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: ContactPerson/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsDeleted")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var dbcontext = repo.UnitOfWork.Context;
                dbcontext.Entry(客戶聯絡人).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }
        public ActionResult BatchEdit(int CustomerId)
        {
            return View();
        }
        // GET: ContactPerson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: ContactPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 l_ContactPerson = repo.Find(id);
            repo.Delete(l_ContactPerson);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
        public ActionResult ExcelOutput()
        {
            var repodata = repo.All();
            var data = from s1 in repodata select new { s1.職稱, s1.姓名, s1.Email, s1.手機, s1.電話, s1.客戶資料.客戶名稱 };
            var l_ExcelClass = new ExcelClass();
            DataTable l_datatable = l_ExcelClass.ToDataTable(data);
            string OutputPath = l_ExcelClass.ExcelOutput(l_datatable, " ContactPerson");
            return File(Server.MapPath(OutputPath), "application/vnd.ms-excel");
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
