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
    public class detalle_facturaController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: detalle_factura
        public ActionResult Index()
        {
            var tb_detalle_factura = db.tb_detalle_factura.Include(t => t.tb_factura).Include(t => t.tb_producto);
            return View(tb_detalle_factura.ToList());
        }

        // GET: detalle_factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_detalle_factura tb_detalle_factura = db.tb_detalle_factura.Find(id);
            if (tb_detalle_factura == null)
            {
                return HttpNotFound();
            }
            return View(tb_detalle_factura);
        }

        [Authorize]
        // GET: detalle_factura/Create
        public ActionResult Create()
        {
            ViewBag.id_factura = new SelectList(db.tb_factura, "id_factura", "id_factura");
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto");
            return View();
        }

        // POST: detalle_factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_detalle_factura,id_factura,id_producto,cantidad")] tb_detalle_factura tb_detalle_factura)
        {
            if (ModelState.IsValid)
            {
                db.tb_detalle_factura.Add(tb_detalle_factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_factura = new SelectList(db.tb_factura, "id_factura", "id_factura", tb_detalle_factura.id_factura);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto", tb_detalle_factura.id_producto);
            return View(tb_detalle_factura);
        }

        [Authorize]
        // GET: detalle_factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_detalle_factura tb_detalle_factura = db.tb_detalle_factura.Find(id);
            if (tb_detalle_factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_factura = new SelectList(db.tb_factura, "id_factura", "id_factura", tb_detalle_factura.id_factura);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto", tb_detalle_factura.id_producto);
            return View(tb_detalle_factura);
        }

        // POST: detalle_factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_detalle_factura,id_factura,id_producto")] tb_detalle_factura tb_detalle_factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_detalle_factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_factura = new SelectList(db.tb_factura, "id_factura", "id_factura", tb_detalle_factura.id_factura);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto", tb_detalle_factura.id_producto);
            return View(tb_detalle_factura);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: detalle_factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_detalle_factura tb_detalle_factura = db.tb_detalle_factura.Find(id);
            if (tb_detalle_factura == null)
            {
                return HttpNotFound();
            }
            return View(tb_detalle_factura);
        }

        // POST: detalle_factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_detalle_factura tb_detalle_factura = db.tb_detalle_factura.Find(id);
            db.tb_detalle_factura.Remove(tb_detalle_factura);
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
