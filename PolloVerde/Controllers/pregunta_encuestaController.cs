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
    public class pregunta_encuestaController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: pregunta_encuesta
        public ActionResult Index()
        {
            return View(db.tb_pregunta_encuesta.ToList());
        }

        // GET: pregunta_encuesta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pregunta_encuesta tb_pregunta_encuesta = db.tb_pregunta_encuesta.Find(id);
            if (tb_pregunta_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_pregunta_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: pregunta_encuesta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pregunta_encuesta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pregunta_encuesta,descripcion_pregunta")] tb_pregunta_encuesta tb_pregunta_encuesta)
        {
            if (ModelState.IsValid)
            {
                db.tb_pregunta_encuesta.Add(tb_pregunta_encuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_pregunta_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: pregunta_encuesta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pregunta_encuesta tb_pregunta_encuesta = db.tb_pregunta_encuesta.Find(id);
            if (tb_pregunta_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_pregunta_encuesta);
        }

        // POST: pregunta_encuesta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pregunta_encuesta,descripcion_pregunta")] tb_pregunta_encuesta tb_pregunta_encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_pregunta_encuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_pregunta_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: pregunta_encuesta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pregunta_encuesta tb_pregunta_encuesta = db.tb_pregunta_encuesta.Find(id);
            if (tb_pregunta_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_pregunta_encuesta);
        }

        // POST: pregunta_encuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_pregunta_encuesta tb_pregunta_encuesta = db.tb_pregunta_encuesta.Find(id);
            db.tb_pregunta_encuesta.Remove(tb_pregunta_encuesta);
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
