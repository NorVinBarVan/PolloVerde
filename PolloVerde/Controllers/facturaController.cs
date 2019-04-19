using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PolloVerde.Contexto;
using PolloVerde.ViewModels;

namespace PolloVerde.Controllers
{
    public class facturaController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: factura
        public ActionResult Index()
        {
            var tb_factura = db.tb_factura.Include(t => t.tb_cliente).Include(t => t.tb_sucursal).Include(t => t.tb_tipo_pago);
            return View(tb_factura.ToList());
        }

        // GET: factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_factura tb_factura = db.tb_factura.Find(id);
            if (tb_factura == null)
            {
                return HttpNotFound();
            }
            return View(tb_factura);
        }

        [Authorize]
        // GET: factura/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente");
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal");
            ViewBag.id_tipo_pago = new SelectList(db.tb_tipo_pago, "id_tipo_pago", "descripcion_tipo_pago");
            ViewBag.productos = new SelectList(db.tb_producto, "id_producto", "descripcion_producto");

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult vistaFactura()
        {
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente");
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal");
            ViewBag.id_tipo_pago = new SelectList(db.tb_tipo_pago, "id_tipo_pago", "descripcion_tipo_pago");
            ViewBag.productos = new SelectList(db.tb_producto, "id_producto", "descripcion_producto");

            ViewBag.items = db.tb_producto.Select(x => new { x.id_producto, x.descripcion_producto, x.precio_producto }).ToList();

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult nuevaFactura(FacturaVM model)
        {

            List<tb_detalle_factura> detalles = new List<tb_detalle_factura>();

            model.items.ForEach(x =>
            {
                tb_detalle_factura nuevo = new tb_detalle_factura()
                {
                    id_producto = x.id_producto,
                    cantidad = x.cantidad
                };
                detalles.Add(nuevo);
            });

            tb_factura nuevaFactura = new tb_factura()
            {
                id_cliente = model.idcliente,
                id_sucursal = model.idsucursal,
                id_tipo_pago = model.idtipopago,
                fecha_creacion = DateTime.Now,
                fecha_modificacion = DateTime.Now,
                descuento_aplicado = model.descuento,
                tb_detalle_factura = detalles
            };

            db.tb_factura.Add(nuevaFactura);
            db.SaveChanges();

            return Json(new { id= nuevaFactura.id_factura }, JsonRequestBehavior.AllowGet);

        }


        // POST: factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_factura,fecha_creacion,fecha_modificacion,descuento_aplicado,id_tipo_pago,id_cliente,id_sucursal")] tb_factura tb_factura)
        {
            if (ModelState.IsValid)
            {
                db.tb_factura.Add(tb_factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente", tb_factura.id_cliente);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_factura.id_sucursal);
            ViewBag.id_tipo_pago = new SelectList(db.tb_tipo_pago, "id_tipo_pago", "descripcion_tipo_pago", tb_factura.id_tipo_pago);
            return View(tb_factura);
        }

        [Authorize]
        // GET: factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_factura tb_factura = db.tb_factura.Find(id);
            if (tb_factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente", tb_factura.id_cliente);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_factura.id_sucursal);
            ViewBag.id_tipo_pago = new SelectList(db.tb_tipo_pago, "id_tipo_pago", "descripcion_tipo_pago", tb_factura.id_tipo_pago);
            return View(tb_factura);
        }

        // POST: factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_factura,fecha_creacion,fecha_modificacion,descuento_aplicado,id_tipo_pago,id_cliente,id_sucursal")] tb_factura tb_factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente", tb_factura.id_cliente);
            ViewBag.id_sucursal = new SelectList(db.tb_sucursal, "id_sucursal", "descripcion_sucursal", tb_factura.id_sucursal);
            ViewBag.id_tipo_pago = new SelectList(db.tb_tipo_pago, "id_tipo_pago", "descripcion_tipo_pago", tb_factura.id_tipo_pago);
            return View(tb_factura);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_factura tb_factura = db.tb_factura.Find(id);
            if (tb_factura == null)
            {
                return HttpNotFound();
            }
            return View(tb_factura);
        }

        // POST: factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_factura tb_factura = db.tb_factura.Find(id);
            db.tb_factura.Remove(tb_factura);
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
