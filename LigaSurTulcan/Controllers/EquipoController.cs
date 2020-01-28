using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LigaSurTulcan.Models;
using System.Web.Helpers;

namespace LigaSurTulcan.Controllers
{
    public class EquipoController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Equipo
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
            var equipo = db.Equipo.Include(e => e.Dirigente);
            return View(equipo.ToList());
        }

        public ActionResult Contar()
        {
            //Contar equipos serie A//
            var equipos = db.Equipo.Count(d => d.serie == "A");
            ViewBag.variable = equipos;

            //Contar Equipos Serie B//
            var equiposb = db.Equipo.Count(d => d.serie == "B");
            ViewBag.variables = equiposb;

            //string[] personas = new string[equiposb];

            //for (int i = 1; i < equipos; i++)
            //{
            //    for (int j = i + 1; j < equipos; j++)
            //    {

            //        ViewBag.e = i + " VS " + j;
            //        personas[i] = ViewBag.e;
                   

            //    }
            //}
            return View();
        }

        public ActionResult Jornadas()
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
            var equipo = db.Equipo.Include(e => e.Dirigente);
            return View(equipo.ToList());
        }



        // GET: Equipo/Create
        public ActionResult Create()
        {
            ViewBag.id_dirigente = new SelectList(db.Dirigente, "Id_dirigente", "Fullname");
            return View();
        }

        // POST: Equipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Equipo,nom_equipo,color_equipo,fundacion,foto_equipo,liga,serie,Estado_equipo,id_dirigente")] Equipo equipo)
        {
          
            HttpPostedFileBase foto_equipo = Request.Files[0];
            string ruta = Server.MapPath("~/Repositorio/");
            ruta += foto_equipo.FileName;
            foto_equipo.SaveAs(ruta);
            equipo.foto_equipo = foto_equipo.FileName;

            if (ModelState.IsValid)
            {
                db.Equipo.Add(equipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_dirigente = new SelectList(db.Dirigente, "Id_dirigente", "Fullname", equipo.id_dirigente);
            return View(equipo);
        }

        // GET: Equipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_dirigente = new SelectList(db.Dirigente, "Id_dirigente", "Fullname", equipo.id_dirigente);
            return View(equipo);
        }

        // POST: Equipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Equipo,nom_equipo,color_equipo,fundacion,foto_equipo,liga,serie,Estado_equipo,id_dirigente")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_dirigente = new SelectList(db.Dirigente, "Id_dirigente", "Fullname", equipo.id_dirigente);
            return View(equipo);
        }



        // GET: Equipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Equipo equipo = db.Equipo.Find(id);
                db.Equipo.Remove(equipo);
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

        // POST: Equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            db.Equipo.Remove(equipo);
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
