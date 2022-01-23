using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SalesSystema.Areas.Users.Models
{
    public class InputModelLogin
    {
        [Required(ErrorMessage = "El campo correo Electronico es Obligatorio")]
        [StringLength(100, ErrorMessage = "El correo electronico no es una direccion de Correo Electronico Valida")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo contrasena es Obligatorio")]
        [StringLength(100, ErrorMessage = "El numero de caracteres de {0} debe ser al menos{2}", MinimumLength = 6)]
        public string Password { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
