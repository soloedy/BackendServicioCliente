using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Visitas
{
    public class visitaDetalleViewModel
    {
        [Required]
        [ForeignKey("visita")]
        public int vis_id { get; set; }
        public int visd_correlativo { get; set; }
        public string visd_observaciones { get; set; }
        public sc_visita visita { get; set; }
    }
}
