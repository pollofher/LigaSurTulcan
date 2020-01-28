using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LigaSurTulcan.Models;

namespace LigaSurTulcan.Controllers
{
    public class GolesController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Goles
        public ActionResult Index()
        {
            var gol_jugador_partido = db.Gol_jugador_partido.Include(g => g.Jugador).Include(g => g.Partido);
            return View(gol_jugador_partido.ToList());
        }

        // GET: Goles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gol_jugador_partido gol_jugador_partido = db.Gol_jugador_partido.Find(id);
            if (gol_jugador_partido == null)
            {
                return HttpNotFound();
            }
            return View(gol_jugador_partido);
        }

        // GET: Goles/Create
        public ActionResult Create()
        {
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador");
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada");
            return View();
        }

        // POST: Goles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_gol,id_partido,id_jugador,goles")] Gol_jugador_partido gol_jugador_partido)
        {
            if (ModelState.IsValid)
            {
                db.Gol_jugador_partido.Add(gol_jugador_partido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador", gol_jugador_partido.id_jugador);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", gol_jugador_partido.id_partido);
            return View(gol_jugador_partido);
        }

        // GET: Goles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gol_jugador_partido gol_jugador_partido = db.Gol_jugador_partido.Find(id);
            if (gol_jugador_partido == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador", gol_jugador_partido.id_jugador);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", gol_jugador_partido.id_partido);
            return View(gol_jugador_partido);
        }

        // POST: Goles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_gol,id_partido,id_jugador,goles")] Gol_jugador_partido gol_jugador_partido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gol_jugador_partido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador", gol_jugador_partido.id_jugador);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", gol_jugador_partido.id_partido);
            return View(gol_jugador_partido);
        }

        // GET: Goles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gol_jugador_partido gol_jugador_partido = db.Gol_jugador_partido.Find(id);
            if (gol_jugador_partido == null)
            {
                return HttpNotFound();
            }
            return View(gol_jugador_partido);
        }

        // POST: Goles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gol_jugador_partido gol_jugador_partido = db.Gol_jugador_partido.Find(id);
            db.Gol_jugador_partido.Remove(gol_jugador_partido);
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
