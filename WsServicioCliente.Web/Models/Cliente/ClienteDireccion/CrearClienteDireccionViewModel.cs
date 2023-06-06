using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Cliente
{
    public class CrearClienteDireccionViewModel
    {
        public int clie_id { get; set; }
        public string dir_descripcion { get; set; }
        public string dir_direccion { get; set; }
        public float dir_latitud { get; set; }
        public float dir_longitud { get; set; }
        public int dir_id { get; set; }


    }
}
