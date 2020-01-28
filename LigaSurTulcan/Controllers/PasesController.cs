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
    public class PasesController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Pases
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
            var pase = db.Pase.Include(p => p.Equipo).Include(p => p.Jugador);
            return View(pase.ToList());
        }

        // GET: Pases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pase pase = db.Pase.Find(id);
            if (pase == null)
            {
                return HttpNotFound();
            }
            return View(pase);
        }

        // GET: Pases/Create
        public ActionResult Create()
        {
            ViewBag.id_equipo_entra = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo");
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "Fullname");
            return View();
        }

        // POST: Pases/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_pase,fecha,valor,id_equipo_entra,equipo_sale,id_jugador")] Pase pase)
        {
            if (ModelState.IsValid)
            {
                db.Pase.Add(pase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_equipo_entra = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", pase.id_equipo_entra);
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "Fullname", pase.id_jugador);
            return View(pase);
        }

        // GET: Pases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pase pase = db.Pase.Find(id);
            if (pase == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_equipo_entra = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", pase.id_equipo_entra);
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "Fullname", pase.id_jugador);
            return View(pase);
        }

        // POST: Pases/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_pase,fecha,valor,id_equipo_entra,equipo_sale,id_jugador")] Pase pase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_equipo_entra = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", pase.id_equipo_entra);
            ViewBag.id_jugador = new SelectList(db.Jugador, "Id_jugador", "Fullname", pase.id_jugador);
            return View(pase);
        }

        // GET: Pases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Pase pase = db.Pase.Find(id);
                db.Pase.Remove(pase);
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

        // POST: Pases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pase pase = db.Pase.Find(id);
            db.Pase.Remove(pase);
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
