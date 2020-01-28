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
    public class PagosController : Controller
    {
        private BarrialSurEntities1 db = new BarrialSurEntities1();

        // GET: Pagos
        public ActionResult Index()
        {
            var pagos = db.pagos.Include(p => p.Partido_Equipo).Include(p => p.Partido_Jugador);
            return View(pagos.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagos pagos = db.pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            ViewBag.id_part_equipo = new SelectList(db.Partido_Equipo, "id_part_equipo", "sancion");
            ViewBag.id_part_jugador = new SelectList(db.Partido_Jugador, "id_part_jugador", "Tarjeta");
            return View();
        }

        // POST: Pagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pagos,id_part_jugador,id_part_equipo,total,fecha")] pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_part_equipo = new SelectList(db.Partido_Equipo, "id_part_equipo", "sancion", pagos.id_part_equipo);
            ViewBag.id_part_jugador = new SelectList(db.Partido_Jugador, "id_part_jugador", "Tarjeta", pagos.id_part_jugador);
            return View(pagos);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagos pagos = db.pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_part_equipo = new SelectList(db.Partido_Equipo, "id_part_equipo", "sancion", pagos.id_part_equipo);
            ViewBag.id_part_jugador = new SelectList(db.Partido_Jugador, "id_part_jugador", "Tarjeta", pagos.id_part_jugador);
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pagos,id_part_jugador,id_part_equipo,total,fecha")] pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_part_equipo = new SelectList(db.Partido_Equipo, "id_part_equipo", "sancion", pagos.id_part_equipo);
            ViewBag.id_part_jugador = new SelectList(db.Partido_Jugador, "id_part_jugador", "Tarjeta", pagos.id_part_jugador);
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagos pagos = db.pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pagos pagos = db.pagos.Find(id);
            db.pagos.Remove(pagos);
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
