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
    public class respuesta_encuestaController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: respuesta_encuesta
        public ActionResult Index()
        {
            var tb_respuesta_encuesta = db.tb_respuesta_encuesta.Include(t => t.tb_encuesta).Include(t => t.tb_pregunta_encuesta);
            return View(tb_respuesta_encuesta.ToList());
        }

        // GET: respuesta_encuesta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_respuesta_encuesta tb_respuesta_encuesta = db.tb_respuesta_encuesta.Find(id);
            if (tb_respuesta_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_respuesta_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: respuesta_encuesta/Create
        public ActionResult Create()
        {
            ViewBag.id_encuesta = new SelectList(db.tb_encuesta, "id_encuesta", "id_encuesta");
            ViewBag.id_pregunta_encuesta = new SelectList(db.tb_pregunta_encuesta, "id_pregunta_encuesta", "descripcion_pregunta");
            return View();
        }

        // POST: respuesta_encuesta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_respuesta_encuesta,descripcion_respuesta,id_encuesta,id_pregunta_encuesta")] tb_respuesta_encuesta tb_respuesta_encuesta)
        {
            if (ModelState.IsValid)
            {
                db.tb_respuesta_encuesta.Add(tb_respuesta_encuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_encuesta = new SelectList(db.tb_encuesta, "id_encuesta", "id_encuesta", tb_respuesta_encuesta.id_encuesta);
            ViewBag.id_pregunta_encuesta = new SelectList(db.tb_pregunta_encuesta, "id_pregunta_encuesta", "descripcion_pregunta", tb_respuesta_encuesta.id_pregunta_encuesta);
            return View(tb_respuesta_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: respuesta_encuesta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_respuesta_encuesta tb_respuesta_encuesta = db.tb_respuesta_encuesta.Find(id);
            if (tb_respuesta_encuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_encuesta = new SelectList(db.tb_encuesta, "id_encuesta", "id_encuesta", tb_respuesta_encuesta.id_encuesta);
            ViewBag.id_pregunta_encuesta = new SelectList(db.tb_pregunta_encuesta, "id_pregunta_encuesta", "descripcion_pregunta", tb_respuesta_encuesta.id_pregunta_encuesta);
            return View(tb_respuesta_encuesta);
        }

        // POST: respuesta_encuesta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_respuesta_encuesta,descripcion_respuesta,id_encuesta,id_pregunta_encuesta")] tb_respuesta_encuesta tb_respuesta_encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_respuesta_encuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_encuesta = new SelectList(db.tb_encuesta, "id_encuesta", "id_encuesta", tb_respuesta_encuesta.id_encuesta);
            ViewBag.id_pregunta_encuesta = new SelectList(db.tb_pregunta_encuesta, "id_pregunta_encuesta", "descripcion_pregunta", tb_respuesta_encuesta.id_pregunta_encuesta);
            return View(tb_respuesta_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: respuesta_encuesta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_respuesta_encuesta tb_respuesta_encuesta = db.tb_respuesta_encuesta.Find(id);
            if (tb_respuesta_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_respuesta_encuesta);
        }

        // POST: respuesta_encuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_respuesta_encuesta tb_respuesta_encuesta = db.tb_respuesta_encuesta.Find(id);
            db.tb_respuesta_encuesta.Remove(tb_respuesta_encuesta);
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
