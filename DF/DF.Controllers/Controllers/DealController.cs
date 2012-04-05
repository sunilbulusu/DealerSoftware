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
    public class DealController : Controller
    {
        private DealerFinanceContext db = new DealerFinanceContext();

        //
        // GET: /Deal/

        public ViewResult Index()
        {
            var deals = db.Deals.Include(d => d.Buyer).Include(d => d.DealDealership);
            return View(deals.ToList());
        }

        //
        // GET: /Deal/Details/5

        public ViewResult Details(Guid id)
        {
            Deal deal = db.Deals.Find(id);
            return View(deal);
        }

        //
        // GET: /Deal/Create

        public ActionResult Create()
        {
            ViewBag.BuyerID = new SelectList(db.Contacts, "ContactID", "FirstName");
            ViewBag.DealDealershipID = new SelectList(db.Dealerships, "DealershipID", "Name");
            return View();
        } 

        //
        // POST: /Deal/Create

        [HttpPost]
        public ActionResult Create(Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.DealID = Guid.NewGuid();
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.BuyerID = new SelectList(db.Contacts, "ContactID", "FirstName", deal.BuyerID);
            ViewBag.DealDealershipID = new SelectList(db.Dealerships, "DealershipID", "Name", deal.DealDealershipID);
            return View(deal);
        }
        
        //
        // GET: /Deal/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Deal deal = db.Deals.Find(id);
            ViewBag.BuyerID = new SelectList(db.Contacts, "ContactID", "FirstName", deal.BuyerID);
            ViewBag.DealDealershipID = new SelectList(db.Dealerships, "DealershipID", "Name", deal.DealDealershipID);
            return View(deal);
        }

        //
        // POST: /Deal/Edit/5

        [HttpPost]
        public ActionResult Edit(Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerID = new SelectList(db.Contacts, "ContactID", "FirstName", deal.BuyerID);
            ViewBag.DealDealershipID = new SelectList(db.Dealerships, "DealershipID", "Name", deal.DealDealershipID);
            return View(deal);
        }

        //
        // GET: /Deal/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Deal deal = db.Deals.Find(id);
            return View(deal);
        }

        //
        // POST: /Deal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
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