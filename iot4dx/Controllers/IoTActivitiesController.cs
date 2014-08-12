using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iot4dx.DataObjects;
using iot4dx.Models;

namespace iot4dx.Controllers
{
    public class IoTActivitiesController : Controller
    {
        private MobileServiceDBContext db = new MobileServiceDBContext();

        // GET: IoTActivities
        public async Task<ActionResult> Index()
        {
            return View(await db.IoTActivities.OrderBy(iot => iot.CreatedAt).ToListAsync());
        }

        // GET: IoTActivities/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IoTActivity ioTActivity = await db.IoTActivities.FindAsync(id);
            if (ioTActivity == null)
            {
                return HttpNotFound();
            }
            return View(ioTActivity);
        }

        [Authorize(Roles = "Administrators")]
        // GET: IoTActivities/Create
        public ActionResult Create()
        {
            return View(new IoTActivity());
        }

        [Authorize(Roles = "Administrators")]
        // POST: IoTActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Color,User,UriImage,Complete,CreatedAt,Deleted,UpdatedAt,Version")] IoTActivity ioTActivity)
        {
            if (ModelState.IsValid)
            {
                db.IoTActivities.Add(ioTActivity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ioTActivity);
        }

        [Authorize(Roles = "Administrators")]
        // GET: IoTActivities/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IoTActivity ioTActivity = await db.IoTActivities.FindAsync(id);
            if (ioTActivity == null)
            {
                return HttpNotFound();
            }
            return View(ioTActivity);
        }

        // POST: IoTActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Color,User,UriImage,Complete,CreatedAt,Deleted,UpdatedAt,Version")] IoTActivity ioTActivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ioTActivity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ioTActivity);
        }

        [Authorize(Roles = "Administrators")]
        // GET: IoTActivities/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IoTActivity ioTActivity = await db.IoTActivities.FindAsync(id);
            if (ioTActivity == null)
            {
                return HttpNotFound();
            }
            return View(ioTActivity);
        }

        [Authorize(Roles = "Administrators")]
        // POST: IoTActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            IoTActivity ioTActivity = await db.IoTActivities.FindAsync(id);
            db.IoTActivities.Remove(ioTActivity);
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
