using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Datos.Mapping.Cliente
{
    public class sc_TipoIdentificacionMap : IEntityTypeConfiguration<sc_TipoIdentificacion>
    {
        public void Configure(EntityTypeBuilder<sc_TipoIdentificacion> builder)
        {
            builder.ToTable("sc_TipoIdentificacion")
                .HasKey(ti => ti.ide_id);
        }
    }
}
