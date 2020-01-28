using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LigaSurTulcan.Models;
using LigaSurTulcan.Models.ViewModels;

namespace LigaSurTulcan.Controllers
{
    [Authorize(Users ="Admin")]
    public class CampeonatoController : Controller
    {
        // GET: Campeonato
        
        public ActionResult Index()
        {

            try
            {
                ViewBag.sms = TempData["sms"].ToString();
            }
            catch { }

            List<ListCampeonatoViewModel> lst;
            using (BarrialSurEntities1 db = new BarrialSurEntities1())
            {
                lst = (from d in db.Campeonato
                       select new ListCampeonatoViewModel
                       {
                           Id_campeonato = d.Id_campeonato,
                           Nom_Campeonato = d.Nom_Campeonato,
                           fecha_ini= d.fecha_ini,
                           fecha_fin = d.fecha_fin,
                           Estado_campeonato = d.Estado_campeonato

                       }).ToList();
            }
            
            return View(lst);
        }
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(CampeonatoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BarrialSurEntities1 db = new BarrialSurEntities1())
                    {
                        var oCampeonato = new Campeonato();
                        oCampeonato.Nom_Campeonato= model.Nom_Campeonato;
                        oCampeonato.fecha_ini = model.fecha_ini;
                        oCampeonato.fecha_fin = model.fecha_fin;
                        oCampeonato.Estado_campeonato = model.Estado_campeonato;

                        db.Campeonato.Add(oCampeonato);
                        db.SaveChanges();

                    }
                    return Redirect("/Campeonato");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public ActionResult Editar(int Id)
        {
            CampeonatoViewModel model = new CampeonatoViewModel();
            using (BarrialSurEntities1 db = new BarrialSurEntities1())
            {
                var oCampeonato = db.Campeonato.Find(Id);
                model.Nom_Campeonato = oCampeonato.Nom_Campeonato;
                model.fecha_ini = oCampeonato.fecha_ini;
                model.fecha_fin = oCampeonato.fecha_fin;
                model.Estado_campeonato = oCampeonato.Estado_campeonato;
                model.Id_campeonato = oCampeonato.Id_campeonato;

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Editar(CampeonatoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BarrialSurEntities1 db = new BarrialSurEntities1())
                    {
                        var oCampeonato = db.Campeonato.Find(model.Id_campeonato);
                        oCampeonato.Nom_Campeonato = model.Nom_Campeonato;
                        oCampeonato.fecha_ini = model.fecha_ini;
                        oCampeonato.fecha_fin= model.fecha_fin;
                        oCampeonato.Estado_campeonato = model.Estado_campeonato;

                        db.Entry(oCampeonato).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Redirect("/Campeonato");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        [HttpGet]
        public ActionResult Eliminar(int Id)
        {

            using (BarrialSurEntities1 db = new BarrialSurEntities1())
            {


                try
                {
                    var oCampeonato = db.Campeonato.Find(Id);
                    db.Campeonato.Remove(oCampeonato);
                    db.SaveChanges();
                    TempData["sms"] = "El dato se elimino correctamente";
                    ViewBag.sms = TempData["sms"];
                    return Redirect("/Campeonato");
                }
                catch
                {
                    TempData["sms"] = "No se puede eliminar porque se encuentra relacionado con otros registros";
                    ViewBag.sms = TempData["sms"];

                    return Redirect("/Campeonato");

                }



            }

        }
    }
}