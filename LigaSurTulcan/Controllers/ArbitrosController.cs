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
    public class ArbitrosController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Arbitros
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
            return View(db.Arbitro.ToList());
        }

        // GET: Arbitros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbitro arbitro = db.Arbitro.Find(id);
            if (arbitro == null)
            {
                return HttpNotFound();
            }
            return View(arbitro);
        }

        // GET: Arbitros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arbitros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_arbitro,ced_arbitro,nom_arbitro,apell_arbitro,dir_arbitro,Pais,telf_arbitro,correo_arbitro")] Arbitro arbitro)
        {
            if (ModelState.IsValid)
            {
                db.Arbitro.Add(arbitro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arbitro);
        }

        // GET: Arbitros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbitro arbitro = db.Arbitro.Find(id);
            if (arbitro == null)
            {
                return HttpNotFound();
            }
            return View(arbitro);
        }

        // POST: Arbitros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_arbitro,ced_arbitro,nom_arbitro,apell_arbitro,dir_arbitro,Pais,telf_arbitro,correo_arbitro")] Arbitro arbitro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arbitro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arbitro);
        }

        // GET: Arbitros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Arbitro arbitro = db.Arbitro.Find(id);
                db.Arbitro.Remove(arbitro);
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
        // POST: Arbitros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arbitro arbitro = db.Arbitro.Find(id);
            db.Arbitro.Remove(arbitro);
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
