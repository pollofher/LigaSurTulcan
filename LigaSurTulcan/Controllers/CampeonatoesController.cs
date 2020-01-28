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
    public class CampeonatoesController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Campeonatoes
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

            return View(db.Campeonato.ToList());
        }

        // GET: Campeonatoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonato.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            return View(campeonato);
        }

        // GET: Campeonatoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campeonatoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_campeonato,Nom_Campeonato,fecha_ini,fecha_fin,Estado_campeonato")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                db.Campeonato.Add(campeonato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campeonato);
        }

        // GET: Campeonatoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonato.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            return View(campeonato);
        }

        // POST: Campeonatoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_campeonato,Nom_Campeonato,fecha_ini,fecha_fin,Estado_campeonato")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campeonato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campeonato);
        }

        // GET: Campeonatoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Campeonato campeonato = db.Campeonato.Find(id);
                db.Campeonato.Remove(campeonato);
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


        // POST: Campeonatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campeonato campeonato = db.Campeonato.Find(id);
            db.Campeonato.Remove(campeonato);
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
