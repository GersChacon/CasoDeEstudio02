using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc; 

namespace CasoDeEstudio02.Models
{
    public class AlquilarCasaViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Debe seleccionar una casa.")]
        public long IdCasa { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Precio mensual")]
        public decimal PrecioCasa { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "El usuario de alquiler es obligatorio.")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Usuario que alquila")]
        public string UsuarioAlquiler { get; set; }

        public List<SelectListItem> CasasDisponibles { get; set; } = new List<SelectListItem>();
    }
}
