using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WsServicioCliente.Entidades.Usuarios
{
    public class sc_rol
    {
        [Key]
        public int rol_id { get; set; }
        [Required]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "El nombre no debe llevar más de 30 carácteres y menos de 3 carácteres.")]
        public string rol_nombre { get; set; }
        [StringLength(256)]
        public string rol_descripcion { get; set; }

        public bool rol_estado { get; set; }

    }
}
