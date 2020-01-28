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
    public class InscripcionController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Inscripcion
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
            try
            {
                ViewBag.smsbien = TempData["smsbien"].ToString();

            }
            catch { }
            var campeonato_Equipo = db.Campeonato_Equipo.Include(c => c.Campeonato).Include(c => c.Equipo);
            return View(campeonato_Equipo.ToList());
        }

        // GET: Inscripcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato_Equipo campeonato_Equipo = db.Campeonato_Equipo.Find(id);
            if (campeonato_Equipo == null)
            {
                return HttpNotFound();
            }
            return View(campeonato_Equipo);
        }

        // GET: Inscripcion/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }

            ViewBag.id_campeonato = new SelectList(db.Campeonato, "Id_campeonato", "Nom_Campeonato");
            ViewBag.id_Equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo");
            return View();
        }

        // POST: Inscripcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_campeonato_equi,id_campeonato,id_Equipo,inscripcion,garantia")] Campeonato_Equipo campeonato_Equipo)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    db.Campeonato_Equipo.Add(campeonato_Equipo);
                    db.SaveChanges();
                    TempData["smsbien"] = "Datos Guardados!";
                    ViewBag.smsbien = TempData["smsbien"];
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["smserror"] = "No se Guardo";
                    ViewBag.smserror = TempData["smserror"];
                    return RedirectToAction("Create");
                }

            }

            ViewBag.id_campeonato = new SelectList(db.Campeonato, "Id_campeonato", "Nom_Campeonato", campeonato_Equipo.id_campeonato);
            ViewBag.id_Equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", campeonato_Equipo.id_Equipo);
            return View(campeonato_Equipo);
        }

        // GET: Inscripcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato_Equipo campeonato_Equipo = db.Campeonato_Equipo.Find(id);
            if (campeonato_Equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_campeonato = new SelectList(db.Campeonato, "Id_campeonato", "Nom_Campeonato", campeonato_Equipo.id_campeonato);
            ViewBag.id_Equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", campeonato_Equipo.id_Equipo);
            return View(campeonato_Equipo);
        }

        // POST: Inscripcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_campeonato_equi,id_campeonato,id_Equipo,inscripcion,garantia")] Campeonato_Equipo campeonato_Equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campeonato_Equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_campeonato = new SelectList(db.Campeonato, "Id_campeonato", "Nom_Campeonato", campeonato_Equipo.id_campeonato);
            ViewBag.id_Equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", campeonato_Equipo.id_Equipo);
            return View(campeonato_Equipo);
        }

        // GET: Inscripcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Campeonato_Equipo campeonato_equipo = db.Campeonato_Equipo.Find(id);
                db.Campeonato_Equipo.Remove(campeonato_equipo);
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

        // POST: Inscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campeonato_Equipo campeonato_Equipo = db.Campeonato_Equipo.Find(id);
            db.Campeonato_Equipo.Remove(campeonato_Equipo);
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
