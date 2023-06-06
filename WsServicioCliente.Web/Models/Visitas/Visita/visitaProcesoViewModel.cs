using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Entidades.Visitas
{
    public class visitaProcesoViewModel
    {
        public int vis_id { get; set; }
        public DateTime vis_fechaVisita { get; set; }
        public TimeSpan vis_horaIngreso { get; set; }
        public int evi_id { get; set; }
    }
}
