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
    public class MultasController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Multas
        public ActionResult Index()
        {
            try
            {
                ViewBag.sms = TempData["sms"].ToString();

            }
            catch { }
            try
            {

                ViewBag.smsok = TempData["smsok"].ToString();
            }
            catch { }
            var partido_Equipo = db.Partido_Equipo.Include(p => p.Equipo).Include(p => p.Partido);
            return View(partido_Equipo.ToList());
        }

        // GET: Multas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido_Equipo partido_Equipo = db.Partido_Equipo.Find(id);
            if (partido_Equipo == null)
            {
                return HttpNotFound();
            }
            return View(partido_Equipo);
        }

        // GET: Multas/Create
        public ActionResult Create()
        {
            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo");
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada");
            return View();
        }

        // POST: Multas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_part_equipo,id_partido,id_equipo,sancion,cantidad,valor,total")] Partido_Equipo partido_Equipo)
        {
            if (ModelState.IsValid)
            {
                db.Partido_Equipo.Add(partido_Equipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", partido_Equipo.id_equipo);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", partido_Equipo.id_partido);
            return View(partido_Equipo);
        }

        // GET: Multas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido_Equipo partido_Equipo = db.Partido_Equipo.Find(id);
            if (partido_Equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", partido_Equipo.id_equipo);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", partido_Equipo.id_partido);
            return View(partido_Equipo);
        }

        // POST: Multas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_part_equipo,id_partido,id_equipo,sancion,cantidad,valor,total")] Partido_Equipo partido_Equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partido_Equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", partido_Equipo.id_equipo);
            ViewBag.id_partido = new SelectList(db.Partido, "id_partido", "Jornada", partido_Equipo.id_partido);
            return View(partido_Equipo);
        }

        // GET: Multas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Partido_Equipo partido_equipo = db.Partido_Equipo.Find(id);
                db.Partido_Equipo.Remove(partido_equipo);
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

        // POST: Multas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partido_Equipo partido_Equipo = db.Partido_Equipo.Find(id);
            db.Partido_Equipo.Remove(partido_Equipo);
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
