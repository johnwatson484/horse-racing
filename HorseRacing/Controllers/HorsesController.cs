using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HorseRacing.DAL;
using HorseRacing.Models;

namespace HorseRacing.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HorsesController : Controller
    {
        private HorseRacingContext db;

        public HorsesController()
        {
            db = new HorseRacingContext();
        }

        public HorsesController(HorseRacingContext context)
        {
            db = context;
        }


        // GET: Horses
        public async Task<ActionResult> Index()
        {
            return View(await db.Horses.OrderBy(x=>x.Name).ToListAsync());
        }        

        // GET: Horses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HorseId,Name,Skill,Description,Image")] Horse horse)
        {
            if (ModelState.IsValid)
            {
                db.Horses.Add(horse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(horse);
        }

        // GET: Horses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horse horse = await db.Horses.FindAsync(id);
            if (horse == null)
            {
                return HttpNotFound();
            }
            return View(horse);
        }

        // POST: Horses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HorseId,Name,Skill,Description,Image")] Horse horse)
        {
            if (ModelState.IsValid)
            {
                db.SetModified(horse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(horse);
        }

        // GET: Horses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horse horse = await db.Horses.FindAsync(id);
            if (horse == null)
            {
                return HttpNotFound();
            }
            return View(horse);
        }

        // POST: Horses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Horse horse = await db.Horses.FindAsync(id);
            db.Horses.Remove(horse);
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
