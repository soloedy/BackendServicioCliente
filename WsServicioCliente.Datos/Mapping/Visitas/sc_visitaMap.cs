using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Visitas;

namespace WsServicioCliente.Datos.Mapping.Visitas
{
    public class sc_visitaMap : IEntityTypeConfiguration<sc_visita>
    {
        public void Configure(EntityTypeBuilder<sc_visita> builder)
        {
            builder.ToTable("sc_visita")
                .HasKey(vis => vis.vis_id);
        }
    }
}
