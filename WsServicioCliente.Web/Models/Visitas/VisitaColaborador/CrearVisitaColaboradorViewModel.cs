using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;
using WsServicioCliente.Entidades.Puestos;

namespace WsServicioCliente.Entidades.Visitas
{
    public class CrearVisitaColaboradorViewModel
    {
        public int vis_id { get; set; }
        public int sup_id { get; set; }
        public int tec_id { get; set; }

    }
}
