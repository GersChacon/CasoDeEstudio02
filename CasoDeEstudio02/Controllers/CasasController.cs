using CasoDeEstudio02.EF;
using CasoDeEstudio02.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Web;

namespace CasoEstudio2.Controllers
{
    public class CasasController : Controller
    {
        private readonly CasoEstudioKNEntities db = new CasoEstudioKNEntities();

        // GET: Casas/Alquilar
        public ActionResult Alquilar()
        {
            var model = new CasasModel
            {
                CasasDisponibles = new SelectList(
                    db.CasasSistema.Where(c => c.UsuarioAlquiler == null).ToList(),
                    "IdCasa", "DescripcionCasa")
            };
            return View(model);
        }

        // POST: Casas/Alquilar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alquilar(CasasModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casa = db.CasasSistema.Find(model.IdCasa);
                    if (casa == null)
                    {
                        ModelState.AddModelError("", "La casa seleccionada no existe.");
                    }
                    else if (casa.UsuarioAlquiler != null)
                    {
                        ModelState.AddModelError("", "La casa seleccionada ya está alquilada.");
                    }
                    else
                    {
                        casa.UsuarioAlquiler = model.UsuarioAlquiler;
                        casa.FechaAlquiler = DateTime.Now;
                        db.SaveChanges();
                        return RedirectToAction("Consulta");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al procesar el alquiler: " + ex.Message);
                }
            }

            // If we got this far, something failed; redisplay form
            model.CasasDisponibles = new SelectList(
                db.CasasSistema.Where(c => c.UsuarioAlquiler == null).ToList(),
                "IdCasa", "DescripcionCasa");
            return View(model);
        }

        // AJAX method to get house price
        public JsonResult ObtenerPrecioCasa(long id)
        {
            var precio = db.CasasSistema
                           .Where(c => c.IdCasa == id)
                           .Select(c => c.PrecioCasa)
                           .FirstOrDefault();
            return Json(precio, JsonRequestBehavior.AllowGet);
        }

        // GET: Casas/Consulta
        public ActionResult Consulta()
        {
            var casas = db.CasasSistema
                .Where(c => c.PrecioCasa >= 115000 && c.PrecioCasa <= 180000)
                .OrderBy(c => c.UsuarioAlquiler != null) // Available houses first
                .Select(c => new CasasConsultaModel
                {
                    DescripcionCasa = c.DescripcionCasa,
                    PrecioCasa = c.PrecioCasa,
                    UsuarioAlquiler = c.UsuarioAlquiler,
                    Estado = c.UsuarioAlquiler == null ? "Disponible" : "Reservada",
                    FechaAlquiler = c.FechaAlquiler.HasValue ? c.FechaAlquiler.Value.ToString("dd/MM/yyyy") : ""
                }).ToList();

            return View(casas);
        }
    }
}

