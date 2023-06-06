using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WsServicioCliente.Entidades.Visitas
{
    public class actualizarVisitaDetalleViewModel
    {
        [Key]
        public int vis_id { get; set; }
        public int visd_correlativo { get; set; }
        public string visd_observaciones { get; set; }
    }
}
