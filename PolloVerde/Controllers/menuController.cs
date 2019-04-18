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
    public class menuController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: menu
        public ActionResult Index()
        {
            var tb_menu = db.tb_menu.Include(t => t.tb_combo).Include(t => t.tb_producto);
            return View(tb_menu.ToList());
        }

        // GET: menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_menu tb_menu = db.tb_menu.Find(id);
            if (tb_menu == null)
            {
                return HttpNotFound();
            }
            return View(tb_menu);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: menu/Create
        public ActionResult Create()
        {
            ViewBag.id_combo = new SelectList(db.tb_combo, "id_combo", "descripcion_combo");
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto");
            return View();
        }

        // POST: menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_menu,descripcion_menu,id_producto,id_combo")] tb_menu tb_menu)
        {
            if (ModelState.IsValid)
            {
                db.tb_menu.Add(tb_menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_combo = new SelectList(db.tb_combo, "id_combo", "descripcion_combo", tb_menu.id_combo);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto", tb_menu.id_producto);
            return View(tb_menu);
        }

        [Authorize]
        // GET: menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_menu tb_menu = db.tb_menu.Find(id);
            if (tb_menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_combo = new SelectList(db.tb_combo, "id_combo", "descripcion_combo", tb_menu.id_combo);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto", tb_menu.id_producto);
            return View(tb_menu);
        }

        // POST: menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_menu,descripcion_menu,id_producto,id_combo")] tb_menu tb_menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_combo = new SelectList(db.tb_combo, "id_combo", "descripcion_combo", tb_menu.id_combo);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_producto", tb_menu.id_producto);
            return View(tb_menu);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_menu tb_menu = db.tb_menu.Find(id);
            if (tb_menu == null)
            {
                return HttpNotFound();
            }
            return View(tb_menu);
        }

        // POST: menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_menu tb_menu = db.tb_menu.Find(id);
            db.tb_menu.Remove(tb_menu);
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
