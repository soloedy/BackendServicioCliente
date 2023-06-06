using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Visitas;

namespace WsServicioCliente.Datos.Mapping.Visitas
{
    public class sc_visitaDetalleMap : IEntityTypeConfiguration<sc_visitadetalle>
    {
        public void Configure(EntityTypeBuilder<sc_visitadetalle> builder)
        {
            builder.ToTable("sc_visitadetalle")
                .HasKey(vd => vd.vis_id);
        }
    }
}
