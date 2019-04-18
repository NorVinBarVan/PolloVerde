using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PolloVerde.Contexto;

namespace PolloVerde.Controllers
{
    public class regionController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: region
        public ActionResult Index()
        {
            return View(db.tb_region.ToList());
        }

        // GET: region/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_region tb_region = db.tb_region.Find(id);
            if (tb_region == null)
            {
                return HttpNotFound();
            }
            return View(tb_region);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: region/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_region,nombre_region")] tb_region tb_region)
        {
            if (ModelState.IsValid)
            {
                db.tb_region.Add(tb_region);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_region);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: region/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_region tb_region = db.tb_region.Find(id);
            if (tb_region == null)
            {
                return HttpNotFound();
            }
            return View(tb_region);
        }

        // POST: region/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_region,nombre_region")] tb_region tb_region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_region);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: region/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_region tb_region = db.tb_region.Find(id);
            if (tb_region == null)
            {
                return HttpNotFound();
            }
            return View(tb_region);
        }

        // POST: region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_region tb_region = db.tb_region.Find(id);
            db.tb_region.Remove(tb_region);
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
