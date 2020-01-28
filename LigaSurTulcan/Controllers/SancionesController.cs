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
    public class SancionesController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Sanciones
        public ActionResult Index()
        {
            try
            {
                ViewBag.sms = TempData["sms"].ToString();

            }
            catch
            { }
            try
            {
                ViewBag.smserror = TempData["smsok"].ToString();

            }
            catch
            { }
            var partido_Jugador = db.Partido_Jugador.Include(p => p.Jugador).Include(p => p.Partido);
            return View(partido_Jugador.ToList());
        }

        // GET: Sanciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido_Jugador partido_Jugador = db.Partido_Jugador.Find(id);
            if (partido_Jugador == null)
            {
                return HttpNotFound();
            }
            return View(partido_Jugador);
        }

        // GET: Sanciones/Create
        public ActionResult Create()
        {
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador");
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada");
            return View();
        }

        // POST: Sanciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_part_jugador,id_partido,id_jugador,Tarjeta,valor,cantidad,Total")] Partido_Jugador partido_Jugador)
        {
            if (ModelState.IsValid)
            {
                db.Partido_Jugador.Add(partido_Jugador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador", partido_Jugador.id_jugador);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", partido_Jugador.id_partido);
            return View(partido_Jugador);
        }

        // GET: Sanciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido_Jugador partido_Jugador = db.Partido_Jugador.Find(id);
            if (partido_Jugador == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador", partido_Jugador.id_jugador);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", partido_Jugador.id_partido);
            return View(partido_Jugador);
        }

        // POST: Sanciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_part_jugador,id_partido,id_jugador,Tarjeta,valor,cantidad,Total")] Partido_Jugador partido_Jugador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partido_Jugador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "ced_jugador", partido_Jugador.id_jugador);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", partido_Jugador.id_partido);
            return View(partido_Jugador);
        }

        // GET: Sanciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Partido_Jugador partido_jugador = db.Partido_Jugador.Find(id);
                db.Partido_Jugador.Remove(partido_jugador);
                db.SaveChanges();
                TempData["smsok"] = "El dato se elimino correctamente";
                ViewBag.smsok = TempData["smsok"];
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["sms"] = "No se puede eliminar porque está relacionado con otros registros";
                ViewBag.sms = TempData["sms"];
                return RedirectToAction("Index");
            }

        }

        // POST: Sanciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partido_Jugador partido_Jugador = db.Partido_Jugador.Find(id);
            db.Partido_Jugador.Remove(partido_Jugador);
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
