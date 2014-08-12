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
using iot4dx.Helper;

namespace iot4dx.Controllers
{
    [Authorize(Roles="Administrators")]
    public class PushMessagesController : Controller
    {
        private MobileServiceDBContext db = new MobileServiceDBContext();

        // GET: PushMessages
        public async Task<ActionResult> Index()
        {
            return View(await db.PushMessages.OrderBy(push => push.Data).ToListAsync());
        }

        // GET: PushMessages/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PushMessage pushMessage = await db.PushMessages.FindAsync(id);
            if (pushMessage == null)
            {
                return HttpNotFound();
            }
            return View(pushMessage);
        }

        // GET: PushMessages/Create
        public ActionResult Create()
        {
            PushMessage message = new PushMessage();
            return View(message);
        }

        // POST: PushMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Mensagem,Agent,Microsoft,Android")] PushMessage pushMessage)
        {
            if (ModelState.IsValid)
            {
                db.PushMessages.Add(pushMessage);
                await db.SaveChangesAsync();

                //await PushHelper.SendPush(pushMessage);

                return RedirectToAction("Index");
            }

            return View(pushMessage);
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
