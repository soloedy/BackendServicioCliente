using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WsServicioCliente.Entidades.Cliente
{
    public class sc_TipoIdentificacion
    {
        [Key]
        public int ide_id { get; set; }
        [Required]
        public string ide_descripcion { get; set; }
        public bool ide_estado { get; set; }
    }
}
