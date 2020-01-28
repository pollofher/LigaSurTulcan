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
    public class JugadorController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Jugador
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
            var jugador = db.Jugador.Include(j => j.Equipo);
            return View(jugador.ToList());
        }

        // GET: Jugador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // GET: Jugador/Create
        public ActionResult Create()
        {
            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo");
            return View();
        }

        // POST: Jugador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_jugador,ced_jugador,nom_jugador,apell_jugador,fechaNac_jugador,carnet_jugador,foto_jugador,fecha_filiacion,estado_civil,instruccion,profesion,provincia,parroquia,id_equipo")] Jugador jugador)
        {
            //Ruta donde se guarda la imagen
            HttpPostedFileBase foto_jugador = Request.Files[0];
            string ruta = Server.MapPath("~/ImgJugador/");
            ruta += foto_jugador.FileName;
            foto_jugador.SaveAs(ruta);

            //Guardar nimbre en la base de datos
            jugador.foto_jugador = foto_jugador.FileName;
            if (ModelState.IsValid)
            {
                db.Jugador.Add(jugador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", jugador.id_equipo);
            return View(jugador);
        }

        // GET: Jugador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", jugador.id_equipo);
            return View(jugador);
        }

        // POST: Jugador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_jugador,ced_jugador,nom_jugador,apell_jugador,fechaNac_jugador,carnet_jugador,foto_jugador,fecha_filiacion,estado_civil,instruccion,profesion,provincia,parroquia,id_equipo")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jugador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "Id_Equipo", "nom_equipo", jugador.id_equipo);
            return View(jugador);
        }

        // GET: Jugador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Jugador jugador = db.Jugador.Find(id);
                db.Jugador.Remove(jugador);
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

        // POST: Jugador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jugador jugador = db.Jugador.Find(id);
            db.Jugador.Remove(jugador);
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
