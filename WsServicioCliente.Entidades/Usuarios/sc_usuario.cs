using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Usuarios
{
    public class sc_usuario
    {
        [Key]
        public int usua_id { get; set; }

        [ForeignKey("rol")]
        public int rol_id { get; set; }
        public string usua_nombre { get; set; }
        public string usua_email { get; set; }
        public byte[] usua_password_hash { get; set; }
        public byte[] usua_password_salt { get; set; }
        public bool usua_estado { get; set; }
        public sc_rol rol { get; set; }

    }
}
