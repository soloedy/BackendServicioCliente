using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Entidades.Visitas
{
    public class ActualizarDireccionVisitaViewModel
    {
        [Key]
        public int vis_id { get; set; }
        public int dir_id { get; set; }
    }
}
