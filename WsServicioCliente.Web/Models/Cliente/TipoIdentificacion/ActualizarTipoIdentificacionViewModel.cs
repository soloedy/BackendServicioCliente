using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WsServicioCliente.Web.Models.Cliente
{
    public class ActualizarTipoIdentificacionViewModel
    {
        public int ide_id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La descripción del tipo de identificación no debe tener más de 50 carácteres.")]
        public string ide_descripcion { get; set; }
        public bool ide_estado { get; set; }
    }
}
