using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WsServicioCliente.Entidades.Puestos
{
    public class ActualizarTecnicoViewModel
    {
        [Key]
        public int tec_id { get; set; }
        [Required]
        public string tec_nombre { get; set; }
        public string tec_correo { get; set; }
        public DateTime tec_fechaNacimiento { get; set; }
        public DateTime tec_fechaIngreso { get; set; }
        public string tec_identificacion { get; set; }
        public string tec_telefono { get; set; }
        public bool tec_estado { get; set; }

        [ForeignKey("supervisor")]
        public int sup_id { get; set; }

        public sc_supervisor supervisor { get; set; }
    }
}
