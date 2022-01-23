using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystema.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage="El campo Nombre es Obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo Apellido es Obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo RFC es Obligatorio")]
        public string NID { get; set; }
        
        [Required(ErrorMessage = "El campo Telefono es Obligatorio")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-.]?([0-9]{2})[-.]?([0-9]{5})$", ErrorMessage ="El formato de telefono ingresado no es valido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El campo correo Electronico es Obligatorio")]
        [StringLength(100,ErrorMessage ="El correo electronico no es una direccion de Correo Electronico Valida")]
        public string Email { get; set; }

        [Display(Name="Contraseña")]
        [Required(ErrorMessage = "El campo contrasena es Obligatorio")]
        [StringLength(100, ErrorMessage = "El numero de caracteres de {0} debe ser al menos{2}", MinimumLength =6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione un Role")]
        public string Role { get; set; }
        public string ID { get; set; }
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
