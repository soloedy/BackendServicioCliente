using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Datos.Mapping.Cliente
{
    public class sc_clientedireccionMap : IEntityTypeConfiguration<sc_clientedireccion>
    {
        public void Configure(EntityTypeBuilder<sc_clientedireccion> builder)
        {
            builder.ToTable("sc_cliente_direccion")
                .HasKey(c => c.clie_id);
        }
    }
}
