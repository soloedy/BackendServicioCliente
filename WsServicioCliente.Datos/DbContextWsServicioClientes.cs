using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WsServicioCliente.Datos.Mapping.Cliente;
using WsServicioCliente.Datos.Mapping.Puestos;
using WsServicioCliente.Datos.Mapping.Usuarios;
using WsServicioCliente.Datos.Mapping.Visitas;
using WsServicioCliente.Entidades.Cliente;
using WsServicioCliente.Entidades.Puestos;
using WsServicioCliente.Entidades.Usuarios;
using WsServicioCliente.Entidades.Visitas;

namespace WsServicioCliente.Datos
{
    public class DbContextWsServicioClientes : DbContext
    {
        public DbSet<sc_TipoIdentificacion> TipoIdenficaciones { get; set; }
        public DbSet<sc_cliente> clientes { get; set; }
        public DbSet<sc_supervisor> supervisores { get; set; }
        public DbSet<sc_tecnico> tecnicos { get; set; }
        public DbSet<sc_estadoVisita> estadoVisitas { get; set; }
        public DbSet<sc_visita> visitas { get; set; }
        public DbSet<sc_visitadetalle> visitadetalles { get; set; }
        public DbSet<sc_visitaColaborador> visitaColaboradores { get; set; }
        public DbSet<sc_rol> roles { get; set; }
        public DbSet <sc_usuario> usuarios { get; set; }
        public DbSet<sc_clientedireccion> direcciones { get; set; }
        public DbContextWsServicioClientes(DbContextOptions<DbContextWsServicioClientes> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new sc_TipoIdentificacionMap());
            modelBuilder.ApplyConfiguration(new sc_clienteMap());
            modelBuilder.ApplyConfiguration(new sc_supervisorMap());
            modelBuilder.ApplyConfiguration(new sc_tecnicoMap());
            modelBuilder.ApplyConfiguration(new sc_estadoVisitaMap());
            modelBuilder.ApplyConfiguration(new sc_visitaMap());
            modelBuilder.ApplyConfiguration(new sc_visitaDetalleMap());
            modelBuilder.ApplyConfiguration(new sc_visitaColaboradorMap());
            modelBuilder.ApplyConfiguration(new sc_rolMap());
            modelBuilder.ApplyConfiguration(new sc_usuarioMap());
            modelBuilder.ApplyConfiguration(new sc_clientedireccionMap());
        }

    }
}
