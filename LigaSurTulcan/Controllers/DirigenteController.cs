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
  
    public class DirigenteController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Dirigente
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
            return View(db.Dirigente.ToList());
        }

        // GET: Dirigente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dirigente dirigente = db.Dirigente.Find(id);
            if (dirigente == null)
            {
                return HttpNotFound();
            }
            return View(dirigente);
        }

        // GET: Dirigente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dirigente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_dirigente,ced_dirigente,nom_dirigente,apelli_dirigente,dir_dirigente,telf_dirigente")] Dirigente dirigente)
        {
            if (ModelState.IsValid)
            {
                db.Dirigente.Add(dirigente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dirigente);
        }

        // GET: Dirigente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dirigente dirigente = db.Dirigente.Find(id);
            if (dirigente == null)
            {
                return HttpNotFound();
            }
            return View(dirigente);
        }

        // POST: Dirigente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_dirigente,ced_dirigente,nom_dirigente,apelli_dirigente,dir_dirigente,telf_dirigente")] Dirigente dirigente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dirigente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dirigente);
        }

        // GET: Dirigente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Dirigente dirigente = db.Dirigente.Find(id);
                db.Dirigente.Remove(dirigente);
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

        // POST: Dirigente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dirigente dirigente = db.Dirigente.Find(id);
            db.Dirigente.Remove(dirigente);
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
