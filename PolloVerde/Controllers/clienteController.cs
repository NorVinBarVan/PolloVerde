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
    public class clienteController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();
        
        [Authorize]
        // GET: cliente
        public ActionResult Index()
        {
            return View(db.tb_cliente.ToList());
        }

        // GET: cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        [Authorize]
        // GET: cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,nit_cliente,nombre_cliente,direccion_cliente")] tb_cliente tb_cliente)
        {
            if (ModelState.IsValid)
            {
                db.tb_cliente.Add(tb_cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_cliente);
        }

        [Authorize]
        // GET: cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        // POST: cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,nit_cliente,nombre_cliente,direccion_cliente")] tb_cliente tb_cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_cliente);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        // POST: cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            db.tb_cliente.Remove(tb_cliente);
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
