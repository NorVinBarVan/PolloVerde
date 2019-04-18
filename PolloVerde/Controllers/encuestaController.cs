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
    public class encuestaController : Controller
    {
        private PolloVerdeEntities db = new PolloVerdeEntities();

        [Authorize]
        // GET: encuesta
        public ActionResult Index()
        {
            var tb_encuesta = db.tb_encuesta.Include(t => t.tb_cliente);
            return View(tb_encuesta.ToList());
        }

        // GET: encuesta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_encuesta tb_encuesta = db.tb_encuesta.Find(id);
            if (tb_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_encuesta);
        }

        [Authorize]
        // GET: encuesta/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente");
            return View();
        }

        // POST: encuesta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_encuesta,fecha_encuesta,id_cliente")] tb_encuesta tb_encuesta)
        {
            if (ModelState.IsValid)
            {
                db.tb_encuesta.Add(tb_encuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente", tb_encuesta.id_cliente);
            return View(tb_encuesta);
        }

        [Authorize]
        // GET: encuesta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_encuesta tb_encuesta = db.tb_encuesta.Find(id);
            if (tb_encuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente", tb_encuesta.id_cliente);
            return View(tb_encuesta);
        }

        // POST: encuesta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_encuesta,fecha_encuesta,id_cliente")] tb_encuesta tb_encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_encuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.tb_cliente, "id_cliente", "nombre_cliente", tb_encuesta.id_cliente);
            return View(tb_encuesta);
        }

        [Authorize(Roles = "administrador")]
        [Authorize]
        // GET: encuesta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_encuesta tb_encuesta = db.tb_encuesta.Find(id);
            if (tb_encuesta == null)
            {
                return HttpNotFound();
            }
            return View(tb_encuesta);
        }

        // POST: encuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_encuesta tb_encuesta = db.tb_encuesta.Find(id);
            db.tb_encuesta.Remove(tb_encuesta);
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

        [Authorize]
        public ActionResult getViewEncuesta()
        {

            var lista_Clientes = db.tb_cliente.ToList();
            SelectList lista = new SelectList(lista_Clientes, "id_cliente", "nombre_cliente");

            ViewBag.lista = lista;
            var preguntas = db.tb_pregunta_encuesta.Select(x => new PreguntaVM() {id_pregunta_encuesta = x.id_pregunta_encuesta, descripcion_pregunta = x.descripcion_pregunta }).ToList();

            ViewBag.preguntas = preguntas;
            ViewBag.ultimoId = preguntas[preguntas.Count() - 1].id_pregunta_encuesta;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult guardarEncuesta(EncuestaVM model)
        {
            try
            {

                List<tb_respuesta_encuesta> lista_respuestas = new List<tb_respuesta_encuesta>();

                model.respuestas.ForEach(x =>
                {
                    var respuesta = new tb_respuesta_encuesta() { id_pregunta_encuesta = x.id_pregunta, descripcion_respuesta = x.descripcion_respuesta };
                    lista_respuestas.Add(respuesta);
                });


                tb_encuesta nuevaEncuestsa = new tb_encuesta() //Aqui solo dejas Encuesta nuevaEncuesta = new Encuesta(); yo lo puse asi porque las tenia en otra ubicacion
                {
                    id_cliente = model.id_cliente,
                    fecha_encuesta = DateTime.Now,
                    tb_respuesta_encuesta = lista_respuestas
                };

                var guardada = db.tb_encuesta.Add(nuevaEncuestsa);
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

        }
    }
}
