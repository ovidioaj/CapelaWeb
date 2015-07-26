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
        public class CapelaGroupsController : Controller
        {
                private LocalDatabase db = new LocalDatabase();

                public async Task<ActionResult> Index()
                {
                        return View(await db.CapelaGroups.ToListAsync());
                }

                public async Task<ActionResult> Details(string name)
                {
                        if (string.IsNullOrEmpty(name)) {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        CapelaGroup capelaGroup = await db.CapelaGroups.FirstOrDefaultAsync(c => c.Name.Equals(name));
                        if (capelaGroup == null) {
                                return HttpNotFound();
                        }
                        return View(capelaGroup);
                }

                // GET: CapelaGroups/Create
                public ActionResult Create()
                {
                        ViewBag.PhotoWarning = false;
                        return View();
                }

                // POST: CapelaGroups/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> Create([Bind(Include = "Id,Title,Name,Text")] CapelaGroup capelaGroup, HttpPostedFileBase image)
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
                                return View(capelaGroup);
                        }
                        capelaGroup.Photo = newImage;

                        if (ModelState.IsValid) {
                                capelaGroup.Id = Guid.NewGuid();
                                db.CapelaGroups.Add(capelaGroup);
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                        }

                        return View(capelaGroup);
                }

                // GET: CapelaGroups/Edit/5
                public async Task<ActionResult> Edit(Guid? id)
                {
                        if (id == null) {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        CapelaGroup capelaGroup = await db.CapelaGroups.FindAsync(id);
                        if (capelaGroup == null) {
                                return HttpNotFound();
                        }

                        ViewBag.PhotoWarning = false;
                        return View(capelaGroup);
                }

                // POST: CapelaGroups/Edit/5
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Name,Text")] CapelaGroup capelaGroup, HttpPostedFileBase image)
                {
                        if (image != null && image.ContentLength > 0) {
                                using (var binaryReader = new BinaryReader(image.InputStream)) {
                                        capelaGroup.Photo = binaryReader.ReadBytes(image.ContentLength);
                                }
                        }
                        else {
                                var local = await db.CapelaGroups.FindAsync(capelaGroup.Id);
                                if (local == null) {
                                        ViewBag.PhotoWarning = true;
                                        return View(capelaGroup);
                                }
                                ViewBag.PhotoWarning = false;
                                capelaGroup.Photo = local.Photo;
                        }

                        if (ModelState.IsValid){
                                db.CapelaGroups.AddOrUpdate(c => c.Id, capelaGroup);
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                        }
                        return View(capelaGroup);
                }

                // GET: CapelaGroups/Delete/5
                public async Task<ActionResult> Delete(Guid? id)
                {
                        if (id == null) {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        CapelaGroup capelaGroup = await db.CapelaGroups.FindAsync(id);
                        if (capelaGroup == null) {
                                return HttpNotFound();
                        }
                        return View(capelaGroup);
                }

                // POST: CapelaGroups/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> DeleteConfirmed(Guid id)
                {
                        CapelaGroup capelaGroup = await db.CapelaGroups.FindAsync(id);
                        db.CapelaGroups.Remove(capelaGroup);
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