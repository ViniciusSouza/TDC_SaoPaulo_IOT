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
    
    public class AgendaEventoesController : Controller
    {
        private MobileServiceDBContext db = new MobileServiceDBContext();

        // GET: AgendaEventoes
        public async Task<ActionResult> Index()
        {
            return View(await db.AgendaEventoes.ToListAsync());
        }

        // GET: AgendaEventoes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendaEvento agendaEvento = await db.AgendaEventoes.FindAsync(id);
            if (agendaEvento == null)
            {
                return HttpNotFound();
            }
            return View(agendaEvento);
        }

        [Authorize(Roles = "Administrators")]
        // GET: AgendaEventoes/Create
        public ActionResult Create()
        {

            return View(new AgendaEvento());
        }

        // POST: AgendaEventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Palestrante,MiniBioPalestrante,UrlSitePalestrante,TwitterPalestrante,UrlFotoPalestrante,Trilha,Titulo,Descricao,Data,HoraInicio,HoraTermino,CreatedAt,Deleted,UpdatedAt,Version")] AgendaEvento agendaEvento)
        {
            if (ModelState.IsValid)
            {
                db.AgendaEventoes.Add(agendaEvento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(agendaEvento);
        }

        [Authorize(Roles = "Administrators")]
        // GET: AgendaEventoes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendaEvento agendaEvento = await db.AgendaEventoes.FindAsync(id);
            if (agendaEvento == null)
            {
                return HttpNotFound();
            }
            return View(agendaEvento);
        }

        [Authorize(Roles = "Administrators")]
        // POST: AgendaEventoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Palestrante,MiniBioPalestrante,UrlSitePalestrante,TwitterPalestrante,UrlFotoPalestrante,Trilha,Titulo,Descricao,Data,HoraInicio,HoraTermino,CreatedAt,Deleted,UpdatedAt,Version")] AgendaEvento agendaEvento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendaEvento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(agendaEvento);
        }

        [Authorize(Roles = "Administrators")]
        // GET: AgendaEventoes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgendaEvento agendaEvento = await db.AgendaEventoes.FindAsync(id);
            if (agendaEvento == null)
            {
                return HttpNotFound();
            }
            return View(agendaEvento);
        }

        [Authorize(Roles = "Administrators")]
        // POST: AgendaEventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AgendaEvento agendaEvento = await db.AgendaEventoes.FindAsync(id);
            db.AgendaEventoes.Remove(agendaEvento);
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
