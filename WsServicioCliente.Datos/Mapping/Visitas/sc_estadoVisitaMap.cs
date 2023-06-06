using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Visitas;

namespace WsServicioCliente.Datos.Mapping.Visitas
{
    public class sc_estadoVisitaMap : IEntityTypeConfiguration<sc_estadoVisita>
    {
        public void Configure(EntityTypeBuilder<sc_estadoVisita> builder)
        {
            builder.ToTable("sc_estadoVisita")
                .HasKey(ev => ev.evi_id);
        }
    }
}
