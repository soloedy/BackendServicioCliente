using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Datos.Mapping.Cliente
{
    public class sc_clienteMap : IEntityTypeConfiguration<sc_cliente>
    {
        public void  Configure(EntityTypeBuilder<sc_cliente> builder)
        {
            builder.ToTable("sc_cliente")
                .HasKey(c => c.clie_id);
        }
    }
}
