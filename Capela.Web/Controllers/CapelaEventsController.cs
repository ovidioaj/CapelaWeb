using Capela.Web.Business;
using Capela.Web.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Capela.Web.Controllers
{
        public class CapelaEventsController : Controller
        {
                private LocalDatabase db = new LocalDatabase();

                // GET: CapelaEvents
                public async Task<ActionResult> Index()
                {
                        return View(await db.CapelaEvents.ToListAsync());
                }

                // GET: CapelaEvents/Details/5
                public async Task<ActionResult> Details(Guid? id)
                {
                        if (id == null) {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        CapelaEvents capelaEvents = await db.CapelaEvents.FindAsync(id);
                        if (capelaEvents == null) {
                                return HttpNotFound();
                        }
                        return View(capelaEvents);
                }

                // GET: CapelaEvents/Create
                public ActionResult Create()
                {
                        ViewBag.PhotoWarning = false;
                        return View();
                }

                // POST: CapelaEvents/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> Create([Bind(Include = "Id,Title,Name,Text")] CapelaEvents capelaEvents, HttpPostedFileBase image)
                {
                        byte[] newImage = null;

                        if (image != null && image.ContentLength > 0) {
                                using (var binaryReader = new BinaryReader(image.InputStream)) {
                                        newImage = binaryReader.ReadBytes(image.ContentLength);
                                }
                                ViewBag.PhotoWarning = false;
                        }
                        else {
                                ViewBag.PhotoWarning = true;
                                return View(capelaEvents);
                        }
                        capelaEvents.Photo = newImage;

                        if (ModelState.IsValid) {
                                capelaEvents.Id = Guid.NewGuid();
                                db.CapelaEvents.Add(capelaEvents);
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                        }

                        return View(capelaEvents);
                }

                // GET: CapelaEvents/Edit/5
                public async Task<ActionResult> Edit(Guid? id)
                {
                        if (id == null) {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        CapelaEvents capelaEvents = await db.CapelaEvents.FindAsync(id);
                        if (capelaEvents == null) {
                                return HttpNotFound();
                        }

                        ViewBag.PhotoWarning = false;
                        return View(capelaEvents);
                }

                // POST: CapelaEvents/Edit/5
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Name,Text")] CapelaEvents capelaEvent, HttpPostedFileBase image)
                {
                        if (image != null && image.ContentLength > 0) {
                                using (var binaryReader = new BinaryReader(image.InputStream)) {
                                        capelaEvent.Photo = binaryReader.ReadBytes(image.ContentLength);
                                }
                        }
                        else {
                                var local = await db.CapelaGroups.FindAsync(capelaEvent.Id);
                                if (local == null) {
                                        ViewBag.PhotoWarning = true;
                                        return View(capelaEvent);
                                }
                                ViewBag.PhotoWarning = false;
                                capelaEvent.Photo = local.Photo;
                        }

                        if (ModelState.IsValid) {
                                db.CapelaEvents.AddOrUpdate(c => c.Id, capelaEvent);
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                        }
                        return View(capelaEvent);
                }

                // GET: CapelaEvents/Delete/5
                public async Task<ActionResult> Delete(Guid? id)
                {
                        if (id == null) {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        CapelaEvents capelaEvents = await db.CapelaEvents.FindAsync(id);
                        if (capelaEvents == null) {
                                return HttpNotFound();
                        }
                        return View(capelaEvents);
                }

                // POST: CapelaEvents/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> DeleteConfirmed(Guid id)
                {
                        CapelaEvents capelaEvents = await db.CapelaEvents.FindAsync(id);
                        db.CapelaEvents.Remove(capelaEvents);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                }

                protected override void Dispose(bool disposing)
                {
                        if (disposing) {
                                db.Dispose();
                        }
                        base.Dispose(disposing);
                }
        }
}