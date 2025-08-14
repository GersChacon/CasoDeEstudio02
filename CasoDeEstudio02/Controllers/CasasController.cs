using CasoDeEstudio02.EF;
using CasoDeEstudio02.Models;
using System;
using System.Linq;
using System.Web.Mvc;

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
            return View();
        }
    }
}
