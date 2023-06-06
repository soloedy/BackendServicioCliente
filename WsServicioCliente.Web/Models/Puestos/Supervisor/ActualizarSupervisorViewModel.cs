using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WsServicioCliente.Entidades.Puestos
{
    public class ActulizarSupervisorViewModel
    {
        [Key]
        public int sup_id { get; set; }
        public string sup_nombre { get; set; }
        public string sup_correo { get; set; }
        public string sup_telefono { get; set; }
        public DateTime sup_fechaNacimiento { get; set; }
        public DateTime sup_fechaIngreso { get; set; }
        public string sup_identificacion { get; set; }
        public bool sup_estado { get; set; }
    }
}
