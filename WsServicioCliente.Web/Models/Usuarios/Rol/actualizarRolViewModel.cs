using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WsServicioCliente.Entidades.Usuarios
{
    public class actualizarRolViewModel
    {
        [Key]
        public int rol_id { get; set; }
        public string rol_nombre { get; set; }
        public string rol_descripcion { get; set; }

        public bool rol_estado { get; set; }

    }
}
