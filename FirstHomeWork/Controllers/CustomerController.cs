using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstHomeWork.Models;
using PagedList;

namespace FirstHomeWork.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerEntities db = new CustomerEntities();
        private int pageSize = 10;
        // GET: Customer
        public ActionResult Index(string search, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var IQContact = db.客戶聯絡人.Include(客 => 客.客戶資料);
            var IQCustpmers = db.客戶資料.Where(p => p.IsDeleted == false);
            if (!string.IsNullOrEmpty(search))
            {
                IQCustpmers = IQCustpmers.Where(p => p.客戶名稱.Contains(search));
            }
            IQCustpmers = IQCustpmers.OrderByDescending(p => p.Id);
            var result = IQCustpmers.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDeleted")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDeleted")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            客戶資料 l_Customer = db.客戶資料.Find(id);
            var l_ListContact = db.客戶聯絡人.Where(p => p.客戶Id == l_Customer.Id).ToList();
            if(l_ListContact!=null)
            {
                foreach(客戶聯絡人 t_Contact in l_ListContact)
                {
                    t_Contact.IsDeleted = true;
                }
            }
            var l_ListAccount = db.客戶銀行資訊.Where(p => p.客戶Id == l_Customer.Id).ToList();
            if (l_ListAccount != null)
            {
                foreach (客戶銀行資訊 t_Account in l_ListAccount)
                {
                    t_Account.IsDeleted = true;
                }
            }
            l_Customer.IsDeleted=true;
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
