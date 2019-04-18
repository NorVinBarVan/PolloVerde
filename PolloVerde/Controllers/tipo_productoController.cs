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
    public class tipo_productoController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();
        
        [Authorize]
        // GET: tipo_producto
        public ActionResult Index()
        {
            return View(db.tb_tipo_producto.ToList());
        }

        // GET: tipo_producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tipo_producto tb_tipo_producto = db.tb_tipo_producto.Find(id);
            if (tb_tipo_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_tipo_producto);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: tipo_producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipo_producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_producto,descripcion_tipo_producto")] tb_tipo_producto tb_tipo_producto)
        {
            if (ModelState.IsValid)
            {
                db.tb_tipo_producto.Add(tb_tipo_producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_tipo_producto);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: tipo_producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tipo_producto tb_tipo_producto = db.tb_tipo_producto.Find(id);
            if (tb_tipo_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_tipo_producto);
        }

        // POST: tipo_producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_producto,descripcion_tipo_producto")] tb_tipo_producto tb_tipo_producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_tipo_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_tipo_producto);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: tipo_producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tipo_producto tb_tipo_producto = db.tb_tipo_producto.Find(id);
            if (tb_tipo_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_tipo_producto);
        }

        // POST: tipo_producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_tipo_producto tb_tipo_producto = db.tb_tipo_producto.Find(id);
            db.tb_tipo_producto.Remove(tb_tipo_producto);
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
