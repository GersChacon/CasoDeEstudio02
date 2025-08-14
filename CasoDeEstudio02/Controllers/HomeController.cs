using CasoDeEstudio02.EF;
using CasoDeEstudio02.Models;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace TrabajoEnClase.Controllers
{
    public class HomeController : Controller
    {

        #region Index

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion


    }
}