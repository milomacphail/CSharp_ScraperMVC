using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSharp_Scraper;

namespace CSharp_Scraper.Controllers
{
    public class MostRecentScrape : Controller
    {
        private StockTableEntities db = new StockTableEntities();

        // GET: MostRecentScrape
        public async Task<ActionResult> Index()
        {
            return View(await db.StockModels.ToListAsync());
        }

        // GET: MostRecentScrape/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockModel stockModel = await db.StockModels.FindAsync(id);
            if (stockModel == null)
            {
                return HttpNotFound();
            }
            return View(stockModel);
        }

        // GET: MostRecentScrape/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MostRecentScrape/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Time_Scraped,Stock_Symbol,Last_Price,Change,Change_Percent,Volume,Shares,Average_Volume,Market_Cap")] StockModel stockModel)
        {
            if (ModelState.IsValid)
            {
                db.StockModels.Add(stockModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stockModel);
        }

        // GET: MostRecentScrape/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockModel stockModel = await db.StockModels.FindAsync(id);
            if (stockModel == null)
            {
                return HttpNotFound();
            }
            return View(stockModel);
        }

        // POST: MostRecentScrape/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Time_Scraped,Stock_Symbol,Last_Price,Change,Change_Percent,Volume,Shares,Average_Volume,Market_Cap")] StockModel stockModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stockModel);
        }

        // GET: MostRecentScrape/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockModel stockModel = await db.StockModels.FindAsync(id);
            if (stockModel == null)
            {
                return HttpNotFound();
            }
            return View(stockModel);
        }

        // POST: MostRecentScrape/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StockModel stockModel = await db.StockModels.FindAsync(id);
            db.StockModels.Remove(stockModel);
            await db.SaveChangesAsync();
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
