using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealerFinance.Models;

namespace DealerFinance.Controllers
{ 
    public class BankLenderController : Controller
    {
        private DealerFinanceContext db = new DealerFinanceContext();

        //
        // GET: /BankLender/

        public ViewResult Index()
        {
            var banklenders = db.BankLenders.Include(b => b.BankLenderAddress).Include(b => b.BankLenderContact);
            return View(banklenders.ToList());
        }

        //
        // GET: /BankLender/Details/5

        public ViewResult Details(Guid id)
        {
            BankLender banklender = db.BankLenders.Find(id);
            return View(banklender);
        }

        //
        // GET: /BankLender/Create

        public ActionResult Create()
        {
            ViewBag.BankLenderAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress");
            ViewBag.BankLenderContactID = new SelectList(db.Contacts, "ContactID", "FirstName");
            return View();
        } 

        //
        // POST: /BankLender/Create

        [HttpPost]
        public ActionResult Create(BankLender banklender)
        {
            if (ModelState.IsValid)
            {
                banklender.BankLenderID = Guid.NewGuid();
                db.BankLenders.Add(banklender);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.BankLenderAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress", banklender.BankLenderAddressID);
            ViewBag.BankLenderContactID = new SelectList(db.Contacts, "ContactID", "FirstName", banklender.BankLenderContactID);
            return View(banklender);
        }
        
        //
        // GET: /BankLender/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            BankLender banklender = db.BankLenders.Find(id);
            ViewBag.BankLenderAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress", banklender.BankLenderAddressID);
            ViewBag.BankLenderContactID = new SelectList(db.Contacts, "ContactID", "FirstName", banklender.BankLenderContactID);
            return View(banklender);
        }

        //
        // POST: /BankLender/Edit/5

        [HttpPost]
        public ActionResult Edit(BankLender banklender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banklender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankLenderAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress", banklender.BankLenderAddressID);
            ViewBag.BankLenderContactID = new SelectList(db.Contacts, "ContactID", "FirstName", banklender.BankLenderContactID);
            return View(banklender);
        }

        //
        // GET: /BankLender/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            BankLender banklender = db.BankLenders.Find(id);
            return View(banklender);
        }

        //
        // POST: /BankLender/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            BankLender banklender = db.BankLenders.Find(id);
            db.BankLenders.Remove(banklender);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}