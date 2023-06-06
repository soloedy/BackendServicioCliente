using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;
using WsServicioCliente.Entidades.Puestos;

namespace WsServicioCliente.Entidades.Visitas
{
    public class sc_visitaColaborador
    {
        [Required]
        [ForeignKey("visita")]
        public int vis_id { get; set; }
        [Required]
        [ForeignKey("supervisor")]
        public int sup_id { get; set; }
        [Required]
        [ForeignKey("tecnico")]
        public int tec_id { get; set; }
        public sc_supervisor supervisor { get; set; }
        public sc_tecnico tecnico { get; set; }
        public sc_visita visita { get; set; }
    }
}
