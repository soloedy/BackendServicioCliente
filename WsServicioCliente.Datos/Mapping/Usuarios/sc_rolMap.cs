using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Usuarios;

namespace WsServicioCliente.Datos.Mapping.Usuarios
{
    public class sc_rolMap : IEntityTypeConfiguration<sc_rol>
    {
        public void Configure(EntityTypeBuilder<sc_rol> builder)
        {
            builder.ToTable("sc_rol")
                .HasKey(rl => rl.rol_id);
        }
    }
}
