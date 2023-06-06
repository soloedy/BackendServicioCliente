using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Usuarios
{
    public class crearUsuarioViewController
    {
        [Key]
        public int usua_id { get; set; }
        public int rol_id { get; set; }
        public string usua_nombre { get; set; }
        public string usua_email { get; set; }
        public string password { get; set; }
        public bool usua_estado { get; set; }

    }
}
