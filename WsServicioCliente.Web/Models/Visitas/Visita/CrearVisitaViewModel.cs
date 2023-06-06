using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Entidades.Visitas
{
    public class CrearVisitaViewModel
    {
        [Key]
        public int vis_id { get; set; }
        public int clie_id { get; set; }
        public string vis_descripcion { get; set; }
        public DateTime vis_fechaProgramada { get; set; }
        public int dir_id { get; set; }
        public int evi_id { get; set; }

    }
}
