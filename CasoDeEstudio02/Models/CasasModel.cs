using System;
using System.Web.Mvc;

namespace CasoDeEstudio02.Models
{
    public class CasasModel
    {
        public long IdCasa { get; set; }
        public string DescripcionCasa { get; set; }
        public decimal PrecioCasa { get; set; }
        public string UsuarioAlquiler { get; set; }
        public DateTime? FechaAlquiler { get; set; }

        public SelectList CasasDisponibles { get; set; }

    }
}
