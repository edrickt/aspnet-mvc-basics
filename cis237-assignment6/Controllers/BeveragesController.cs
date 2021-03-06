﻿// Edrick Tamayo
// Thursday 3:30PM
// 12/18/20
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cis237_assignment6.Models;

namespace cis237_assignment6.Controllers
{
    [Authorize]
    public class BeveragesController : Controller
    {
        private BeverageContext db = new BeverageContext();

        // GET: Beverages
        public ActionResult Index()
        {
            DbSet<Beverage> BeveragesToFilter = db.Beverages;

            string filterName = "";
            string filterMin = "";
            string filterMax = "";
            string filterPack = "";

            decimal min = 0.00m;
            decimal max = 500.00m;

            if (!String.IsNullOrWhiteSpace(
                (string)Session["session_name"])
                )
            {
                filterName = (string)Session["session_name"];
            }
            if (!String.IsNullOrWhiteSpace(
                (string)Session["session_min"])
                )
            {
                filterMin = (string)Session["session_min"];
                min = Decimal.Parse(filterMin);
            }
            if (!String.IsNullOrWhiteSpace(
                (string)Session["session_max"])
                )
            {
                filterMax = (string)Session["session_max"];
                max = Decimal.Parse(filterMax);
            }
            if (!String.IsNullOrWhiteSpace(
                (string)Session["session_pack"])
                )
            {
                filterPack = (string)Session["session_pack"];
            }

            IList<Beverage> finalFiltered = BeveragesToFilter.Where(
                beverage => beverage.price >= min &&
                beverage.price <= max &&
                beverage.name.Contains(filterName) &&
                beverage.pack.Contains(filterPack)
                ).ToList();

            ViewBag.filterName = filterName;
            ViewBag.filterMin = filterMin;
            ViewBag.filterMax = filterMax;
            ViewBag.filterPack = filterPack;

            return View(finalFiltered);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            string name = Request.Form.Get("name");
            string min = Request.Form.Get("min");
            string max = Request.Form.Get("max");
            string pack = Request.Form.Get("pack");

            Session["session_name"] = name;
            Session["session_min"] = min;
            Session["session_max"] = max;
            Session["session_pack"] = pack;

            return RedirectToAction("Index");
        }

        // GET: Beverages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // GET: Beverages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beverages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Beverages.Add(beverage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    db.Beverages.Remove(beverage);
                    return View(beverage);
                }
            }
            return View(beverage);
        }

        // GET: Beverages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beverage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
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
