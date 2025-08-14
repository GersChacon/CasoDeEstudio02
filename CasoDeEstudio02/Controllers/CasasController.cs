using CasoDeEstudio02.EF;
using CasoDeEstudio02.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Web; 

namespace CasoDeEstudio02.Controllers
{
    public class CasasController : Controller
    {
        [HttpGet]
        public ActionResult Consulta()
        {
            using (var dbContext = new CasoEstudioKNEntities())
            {
                var lista = dbContext.CasasSistema
                    .Where(c => c.PrecioCasa >= 115000 && c.PrecioCasa <= 180000)
                    .Select(c => new CasasModel
                    {
                        IdCasa = c.IdCasa,
                        DescripcionCasa = c.DescripcionCasa,
                        PrecioCasa = c.PrecioCasa,
                        UsuarioAlquiler = c.UsuarioAlquiler,
                        FechaAlquiler = c.FechaAlquiler
                    })
                    .ToList();

                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Alquiler()
        {
            using (var dbContext = new CasoEstudioKNEntities())
            {
                var disponibles = dbContext.CasasSistema
                    .Where(c => c.UsuarioAlquiler == null && c.FechaAlquiler == null)
                    .OrderBy(c => c.DescripcionCasa)
                    .ToList();

                var model = new AlquilarCasaViewModel
                {
                    CasasDisponibles = disponibles.Select(c => new SelectListItem
                    {
                        Value = c.IdCasa.ToString(),
                        Text = c.DescripcionCasa
                    }).ToList()
                };

                return View(model);
            }
        }

        [HttpGet]
        public JsonResult PrecioDeCasa(long id)
        {
            using (var dbContext = new CasoEstudioKNEntities())
            {
                var precio = dbContext.CasasSistema
                    .Where(c => c.IdCasa == id)
                    .Select(c => c.PrecioCasa)
                    .FirstOrDefault();

                return Json(precio, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alquiler(AlquilarCasaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                using (var dbContext = new CasoEstudioKNEntities())
                {
                    model.CasasDisponibles = dbContext.CasasSistema
                        .Where(c => c.UsuarioAlquiler == null && c.FechaAlquiler == null)
                        .OrderBy(c => c.DescripcionCasa)
                        .Select(c => new SelectListItem
                        {
                            Value = c.IdCasa.ToString(),
                            Text = c.DescripcionCasa
                        }).ToList();
                }

                return View(model);
            }

            using (var dbContext = new CasoEstudioKNEntities())
            {
                var casa = dbContext.CasasSistema
                    .SingleOrDefault(c => c.IdCasa == model.IdCasa && c.UsuarioAlquiler == null && c.FechaAlquiler == null);

                if (casa == null)
                {
                    ModelState.AddModelError("", "La casa seleccionada ya no está disponible.");
                    model.CasasDisponibles = dbContext.CasasSistema
                        .Where(c => c.UsuarioAlquiler == null && c.FechaAlquiler == null)
                        .OrderBy(c => c.DescripcionCasa)
                        .Select(c => new SelectListItem
                        {
                            Value = c.IdCasa.ToString(),
                            Text = c.DescripcionCasa
                        }).ToList();

                    return View(model);
                }

                casa.UsuarioAlquiler = model.UsuarioAlquiler;
                casa.FechaAlquiler = DateTime.Now;

                dbContext.SaveChanges();
            }

            return RedirectToAction("Consulta");
        }
    }
}

