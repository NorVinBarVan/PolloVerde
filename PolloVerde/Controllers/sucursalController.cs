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
    public class sucursalController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: sucursal
        public ActionResult Index()
        {
            var tb_sucursal = db.tb_sucursal.Include(t => t.tb_region);
            return View(tb_sucursal.ToList());
        }

        // GET: sucursal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sucursal tb_sucursal = db.tb_sucursal.Find(id);
            if (tb_sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_sucursal);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: sucursal/Create
        public ActionResult Create()
        {
            ViewBag.id_region = new SelectList(db.tb_region, "id_region", "nombre_region");
            return View();
        }

        // POST: sucursal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_sucursal,descripcion_sucursal,id_region")] tb_sucursal tb_sucursal)
        {
            if (ModelState.IsValid)
            {
                db.tb_sucursal.Add(tb_sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_region = new SelectList(db.tb_region, "id_region", "nombre_region", tb_sucursal.id_region);
            return View(tb_sucursal);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: sucursal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sucursal tb_sucursal = db.tb_sucursal.Find(id);
            if (tb_sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_region = new SelectList(db.tb_region, "id_region", "nombre_region", tb_sucursal.id_region);
            return View(tb_sucursal);
        }

        // POST: sucursal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_sucursal,descripcion_sucursal,id_region")] tb_sucursal tb_sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_region = new SelectList(db.tb_region, "id_region", "nombre_region", tb_sucursal.id_region);
            return View(tb_sucursal);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: sucursal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sucursal tb_sucursal = db.tb_sucursal.Find(id);
            if (tb_sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_sucursal);
        }

        // POST: sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_sucursal tb_sucursal = db.tb_sucursal.Find(id);
            db.tb_sucursal.Remove(tb_sucursal);
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
