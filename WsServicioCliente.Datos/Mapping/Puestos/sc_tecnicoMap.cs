using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Puestos;

namespace WsServicioCliente.Datos.Mapping.Puestos
{
    public class sc_tecnicoMap : IEntityTypeConfiguration<sc_tecnico>
    {
        public void Configure(EntityTypeBuilder<sc_tecnico> builder)
        {
            builder.ToTable("sc_tecnico")
                .HasKey(tec => tec.tec_id);
        }
    }
}
