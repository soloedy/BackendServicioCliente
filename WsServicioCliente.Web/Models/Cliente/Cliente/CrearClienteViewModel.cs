using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Cliente
{
    public class CrearClienteViewModel
    {
        [Key]
        public int clie_id { get; set; }
        [Required]
        public int ide_id { get; set; }
        public string clie_identificacion { get; set; }
        public string clie_nombre { get; set; }
        public string clie_telefono { get; set; }
        public string clie_correo { get; set; }
        public string clie_direccion { get; set; }
        public string clie_coordenada { get; set; }
        public bool clie_estado { get; set; }
    }
}
