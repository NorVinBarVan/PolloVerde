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
    public class productoController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: producto
        public ActionResult Index()
        {
            var tb_producto = db.tb_producto.Include(t => t.tb_proveedor).Include(t => t.tb_sucursal).Include(t => t.tb_tipo_producto);
            return View(tb_producto.ToList());
        }

        // GET: producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_producto);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: producto/Create
        public ActionResult Create()
        {
            ViewBag.id_proveedor = new SelectList(db.tb_proveedor, "id_proveedor", "nombre_proveedor");
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal");
            ViewBag.id_tipo_producto = new SelectList(db.tb_tipo_producto, "id_tipo_producto", "descripcion_tipo_producto");
            return View();
        }

        // POST: producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,nombre_producto,descripcion_producto,precio_producto,cantidad_producto,id_tipo_producto,id_proveedor,id_sucursal")] tb_producto tb_producto)
        {
            if (ModelState.IsValid)
            {
                db.tb_producto.Add(tb_producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_proveedor = new SelectList(db.tb_proveedor, "id_proveedor", "nombre_proveedor", tb_producto.id_proveedor);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_producto.id_sucursal);
            ViewBag.id_tipo_producto = new SelectList(db.tb_tipo_producto, "id_tipo_producto", "descripcion_tipo_producto", tb_producto.id_tipo_producto);
            return View(tb_producto);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_proveedor = new SelectList(db.tb_proveedor, "id_proveedor", "nombre_proveedor", tb_producto.id_proveedor);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_producto.id_sucursal);
            ViewBag.id_tipo_producto = new SelectList(db.tb_tipo_producto, "id_tipo_producto", "descripcion_tipo_producto", tb_producto.id_tipo_producto);
            return View(tb_producto);
        }

        // POST: producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,nombre_producto,descripcion_producto,precio_producto,cantidad_producto,id_tipo_producto,id_proveedor,id_sucursal")] tb_producto tb_producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.tb_proveedor, "id_proveedor", "nombre_proveedor", tb_producto.id_proveedor);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_producto.id_sucursal);
            ViewBag.id_tipo_producto = new SelectList(db.tb_tipo_producto, "id_tipo_producto", "descripcion_tipo_producto", tb_producto.id_tipo_producto);
            return View(tb_producto);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_producto);
        }

        // POST: producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_producto tb_producto = db.tb_producto.Find(id);
            db.tb_producto.Remove(tb_producto);
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

        //venta de productos

        [Authorize]
        public ActionResult ventaProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_proveedor = new SelectList(db.tb_proveedor, "id_proveedor", "nombre_proveedor", tb_producto.id_proveedor);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_producto.id_sucursal);
            ViewBag.id_tipo_producto = new SelectList(db.tb_tipo_producto, "id_tipo_producto", "descripcion_tipo_producto", tb_producto.id_tipo_producto);
            return View(tb_producto);
        }

        // POST: producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VentaProducto([Bind(Include = "id_producto,nombre_producto,descripcion_producto,precio_producto,cantidad_producto,id_tipo_producto,id_proveedor,id_sucursal")] tb_producto tb_producto, int cantidadProducto)
        {
            if (ModelState.IsValid)
            {
                //actualizar stock
                tb_producto.cantidad_producto = tb_producto.cantidad_producto - cantidadProducto;

                db.Entry(tb_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.tb_proveedor, "id_proveedor", "nombre_proveedor", tb_producto.id_proveedor);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_producto.id_sucursal);
            ViewBag.id_tipo_producto = new SelectList(db.tb_tipo_producto, "id_tipo_producto", "descripcion_tipo_producto", tb_producto.id_tipo_producto);
            return View(tb_producto);
        }

        [Authorize]
        public ActionResult precioDeVenta(int id_Producto, int cantidadProducto)
        {
            var valorProducto = (from producto in db.tb_producto
                                 where producto.id_producto == id_Producto
                                 select producto.precio_producto).FirstOrDefault();

            int valor = int.Parse(valorProducto.ToString());

            return Content((valor * cantidadProducto).ToString());
        }

    }
}
