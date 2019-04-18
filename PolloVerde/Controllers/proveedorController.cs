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
    public class proveedorController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();
        
        [Authorize]
        // GET: proveedor
        public ActionResult Index()
        {
            var tb_proveedor = db.tb_proveedor.Include(t => t.tb_sucursal);
            return View(tb_proveedor.ToList());
        }

        // GET: proveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_proveedor tb_proveedor = db.tb_proveedor.Find(id);
            if (tb_proveedor == null)
            {
                return HttpNotFound();
            }
            return View(tb_proveedor);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: proveedor/Create
        public ActionResult Create()
        {
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal");
            return View();
        }

        // POST: proveedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_proveedor,nombre_proveedor,telefono_proveedor,direccion_proveedor,id_sucursal")] tb_proveedor tb_proveedor)
        {
            if (ModelState.IsValid)
            {
                db.tb_proveedor.Add(tb_proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_proveedor.id_sucursal);
            return View(tb_proveedor);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: proveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_proveedor tb_proveedor = db.tb_proveedor.Find(id);
            if (tb_proveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_proveedor.id_sucursal);
            return View(tb_proveedor);
        }

        // POST: proveedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_proveedor,nombre_proveedor,telefono_proveedor,direccion_proveedor,id_sucursal")] tb_proveedor tb_proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_proveedor.id_sucursal);
            return View(tb_proveedor);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: proveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_proveedor tb_proveedor = db.tb_proveedor.Find(id);
            if (tb_proveedor == null)
            {
                return HttpNotFound();
            }
            return View(tb_proveedor);
        }

        // POST: proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_proveedor tb_proveedor = db.tb_proveedor.Find(id);
            db.tb_proveedor.Remove(tb_proveedor);
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
