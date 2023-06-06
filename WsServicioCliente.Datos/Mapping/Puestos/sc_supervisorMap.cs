using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Puestos;

namespace WsServicioCliente.Datos.Mapping.Puestos
{
    public class sc_supervisorMap : IEntityTypeConfiguration<sc_supervisor>
    {
        public void Configure(EntityTypeBuilder<sc_supervisor> builder)
        {
            builder.ToTable("sc_supervisor")
                .HasKey(sup => sup.sup_id);
        }
    }
}
