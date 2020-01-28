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
    public class VocalController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Vocal
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
            return View(db.Vocal.ToList());
        }

        // GET: Vocal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vocal vocal = db.Vocal.Find(id);
            if (vocal == null)
            {
                return HttpNotFound();
            }
            return View(vocal);
        }

        // GET: Vocal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vocal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_vocal,ced_vocal,nom_vocal,apell_vocal,telf_vocal,correo_vocal")] Vocal vocal)
        {
            if (ModelState.IsValid)
            {
                db.Vocal.Add(vocal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vocal);
        }

        // GET: Vocal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vocal vocal = db.Vocal.Find(id);
            if (vocal == null)
            {
                return HttpNotFound();
            }
            return View(vocal);
        }

        // POST: Vocal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_vocal,ced_vocal,nom_vocal,apell_vocal,telf_vocal,correo_vocal")] Vocal vocal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vocal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vocal);
        }

        // GET: Vocal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Vocal vocal= db.Vocal.Find(id);
                db.Vocal.Remove(vocal);
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

        // POST: Vocal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vocal vocal = db.Vocal.Find(id);
            db.Vocal.Remove(vocal);
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
