using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Entidades.Visitas
{
    public class sc_visita
    {
        [Key]
        public int vis_id { get; set; }
        [Required]
        [ForeignKey("cliente")]
        public int clie_id { get; set; }
        public DateTime vis_fechaProgramada { get; set; }
        public DateTime vis_fechaVisita { get; set; }
        public TimeSpan vis_horaIngreso { get; set; }
        public TimeSpan vis_horaSalida { get; set; }

        public string vis_descripcion { get; set; }
        public string vis_resolucion { get; set; }
        [Required]
        [ForeignKey("direccion")]
        public int dir_id { get; set; }
        [Required]
        [ForeignKey("estadoVisita")]
        public int evi_id { get; set; }
        public sc_cliente cliente { get; set; }
        public sc_estadoVisita estadoVisita { get; set; }
        public sc_clientedireccion direccion { get; set; }
    }
}
