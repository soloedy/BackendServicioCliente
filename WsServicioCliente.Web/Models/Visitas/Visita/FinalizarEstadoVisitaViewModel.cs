using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Entidades.Visitas
{
    public class FinalizarEstadoVisitaViewModel
    {
        [Key]
        public int vis_id { get; set; }
        public int evi_id { get; set; }

        public string vis_resolucion { get; set; }
    }
}
