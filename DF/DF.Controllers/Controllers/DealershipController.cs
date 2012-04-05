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
    public class DealershipController : Controller
    {
        private DealerFinanceContext db = new DealerFinanceContext();

        //
        // GET: /Dealership/

        public ViewResult Index(Guid? dealershipID)
        {
            var dealerships = db.Dealerships
                    .OrderBy(d => d.Name)
                    .Include(d => d.DealershipAddress)
                    .Include(d => d.DealershipContact);

            //ViewData["Dealerships"] = new SelectList(dealerships, "DealershipID", "Name");

            if(dealershipID != null && dealershipID != Guid.Empty)
            {
                ViewBag.DealershipID = dealershipID;

                dealerships = db.Dealerships
                    .OrderBy(d=>d.Name)
                    .Include(d => d.DealershipAddress)
                    .Include(d => d.DealershipContact)
                    .Where(d=>d.DealershipID == dealershipID);

                return View(dealerships.ToList());
            }
            
            return View(dealerships.ToList());

        }

        //
        // GET: /Dealership/Details/5

        public ViewResult Details(Guid id)
        {
            Dealership dealership = db.Dealerships.Find(id);
            return View(dealership);
        }

        //
        // GET: /Dealership/Create

        public ActionResult Create()
        {
            ViewBag.DealershipAddressID = new SelectList(db.Addresses, "AddressID", "AddressLine1");
            ViewBag.DealershipContactID = new SelectList(db.Contacts, "ContactID", "FirstName");
            return View();
        } 

        //
        // POST: /Dealership/Create

        [HttpPost]
        public ActionResult Create(Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                dealership.DealershipID = Guid.NewGuid();
                db.Dealerships.Add(dealership);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DealershipAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress", dealership.DealershipAddressID);
            ViewBag.DealershipContactID = new SelectList(db.Contacts, "ContactID", "FirstName", dealership.DealershipContactID);
            return View(dealership);
        }
        
        //
        // GET: /Dealership/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Dealership dealership = db.Dealerships.Find(id);
            ViewBag.DealershipAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress", dealership.DealershipAddressID);
            ViewBag.DealershipContactID = new SelectList(db.Contacts, "ContactID", "FirstName", dealership.DealershipContactID);
            return View(dealership);
        }

        //
        // POST: /Dealership/Edit/5

        [HttpPost]
        public ActionResult Edit(Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealershipAddressID = new SelectList(db.Addresses, "AddressID", "FullAddress", dealership.DealershipAddressID);
            ViewBag.DealershipContactID = new SelectList(db.Contacts, "ContactID", "FirstName", dealership.DealershipContactID);
            return View(dealership);
        }

        //
        // GET: /Dealership/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Dealership dealership = db.Dealerships.Find(id);
            return View(dealership);
        }

        //
        // POST: /Dealership/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Dealership dealership = db.Dealerships.Find(id);
            db.Dealerships.Remove(dealership);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SearchIndex(string dealerName)
        {
            var dealershipList = db.Dealerships.OrderBy(d => d.Name);
            ViewData["Dealerships"] = new SelectList(dealershipList);

            var dealers = from d in db.Dealerships
                          select d;

            //if(!String.IsNullOrEmpty(searchString))
            //{
            //    dealers = dealers.Where(s => s.Name.Contains(searchString));
            //}

            //if (!String.IsNullOrEmpty(dealershipName))
            //{
            //    return View(dealers);
            //}
            //else
            //{
            return View(dealers.Where(x => x.Name == dealerName));
            

        }


    }
}