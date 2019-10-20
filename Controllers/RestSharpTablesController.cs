using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSharp_Scraper.Models;

namespace CSharp_Scraper.Controllers
{
    public class RestSharpTablesController : Controller
    {
        private RestSharpTableEntities db = new RestSharpTableEntities();

        // GET: RestSharpTables
        public async Task<ActionResult> Index()
        {
            return View(await db.RestSharpTables.ToListAsync());
        }

        // GET: RestSharpTables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestSharpTable restSharpTable = await db.RestSharpTables.FindAsync(id);
            if (restSharpTable == null)
            {
                return HttpNotFound();
            }
            return View(restSharpTable);
        }

        // GET: RestSharpTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestSharpTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TimeScraped,Symbol,LastPrice,Change,ChangePercent")] RestSharpTable restSharpTable)
        {
            if (ModelState.IsValid)
            {
                db.RestSharpTables.Add(restSharpTable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(restSharpTable);
        }

        // GET: RestSharpTables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestSharpTable restSharpTable = await db.RestSharpTables.FindAsync(id);
            if (restSharpTable == null)
            {
                return HttpNotFound();
            }
            return View(restSharpTable);
        }

        // POST: RestSharpTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TimeScraped,Symbol,LastPrice,Change,ChangePercent")] RestSharpTable restSharpTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restSharpTable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(restSharpTable);
        }

        // GET: RestSharpTables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestSharpTable restSharpTable = await db.RestSharpTables.FindAsync(id);
            if (restSharpTable == null)
            {
                return HttpNotFound();
            }
            return View(restSharpTable);
        }

        // POST: RestSharpTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RestSharpTable restSharpTable = await db.RestSharpTables.FindAsync(id);
            db.RestSharpTables.Remove(restSharpTable);
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


        public ActionResult NewApiCall()
        {
            ApiCall newScrape = new ApiCall();
            newScrape.RestScrape();

            return RedirectToAction("Index");


        }
    }
}
