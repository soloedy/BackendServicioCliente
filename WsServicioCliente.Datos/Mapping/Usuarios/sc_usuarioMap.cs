using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Usuarios;

namespace WsServicioCliente.Datos.Mapping.Usuarios
{
    public class sc_usuarioMap : IEntityTypeConfiguration<sc_usuario>
    {
        public void Configure(EntityTypeBuilder<sc_usuario> builder)
        {
            builder.ToTable("sc_usuario")
                .HasKey(us => us.usua_id);
        }
    }
}
