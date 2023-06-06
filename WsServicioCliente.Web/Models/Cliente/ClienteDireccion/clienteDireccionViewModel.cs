using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Cliente
{
    public class clienteDireccionViewModel
    {
        [Required]
        [ForeignKey("cliente")]
        public int clie_id { get; set; }
        public string dir_descripcion { get; set; }
        public string dir_direccion { get; set; }
        public double dir_latitud { get; set; }
        public double dir_longitud { get; set; }

        public int dir_id { get; set; }
        public sc_cliente cliente { get; set; }
    }
}
