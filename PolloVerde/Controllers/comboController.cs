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
    public class comboController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: combo
        public ActionResult Index()
        {
            return View(db.tb_combo.ToList());
        }

        // GET: combo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_combo tb_combo = db.tb_combo.Find(id);
            if (tb_combo == null)
            {
                return HttpNotFound();
            }
            return View(tb_combo);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: combo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: combo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_combo,descripcion_combo")] tb_combo tb_combo)
        {
            if (ModelState.IsValid)
            {
                db.tb_combo.Add(tb_combo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_combo);
        }

        [Authorize]
        // GET: combo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_combo tb_combo = db.tb_combo.Find(id);
            if (tb_combo == null)
            {
                return HttpNotFound();
            }
            return View(tb_combo);
        }

        // POST: combo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_combo,descripcion_combo")] tb_combo tb_combo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_combo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_combo);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: combo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_combo tb_combo = db.tb_combo.Find(id);
            if (tb_combo == null)
            {
                return HttpNotFound();
            }
            return View(tb_combo);
        }

        // POST: combo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_combo tb_combo = db.tb_combo.Find(id);
            db.tb_combo.Remove(tb_combo);
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
