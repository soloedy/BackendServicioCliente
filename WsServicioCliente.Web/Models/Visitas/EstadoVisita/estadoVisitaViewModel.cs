using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WsServicioCliente.Entidades.Visitas
{
    public class estadoVisitaViewModel
    {
        [Key]
        public int evi_id { get; set; }
        public string evi_nombre { get; set; }
        public string evi_descripcion { get; set; }
    }
}
