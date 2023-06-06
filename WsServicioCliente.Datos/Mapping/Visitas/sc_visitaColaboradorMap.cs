using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Visitas;

namespace WsServicioCliente.Datos.Mapping.Visitas
{
    public class sc_visitaColaboradorMap : IEntityTypeConfiguration<sc_visitaColaborador>
    {
        public void Configure(EntityTypeBuilder<sc_visitaColaborador> builder)
        {
            builder.ToTable("sc_visitaColaborador")
                .HasKey(vc => vc.vis_id);
        }
    }
}
