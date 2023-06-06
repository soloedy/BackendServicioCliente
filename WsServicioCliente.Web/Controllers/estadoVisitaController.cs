using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsServicioCliente.Datos;
using WsServicioCliente.Entidades.Visitas;

namespace WsServicioCliente.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estadoVisitaController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public estadoVisitaController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/estadoVisita/listasr
        [HttpGet("[action]")]
        public async Task<IEnumerable<estadoVisitaViewModel>> listar()
        {
            var estados = await _context.estadoVisitas.ToListAsync();

            return estados.Select(estados => new estadoVisitaViewModel
            {
                evi_id = estados.evi_id,
                evi_descripcion = estados.evi_descripcion,
                evi_nombre = estados.evi_nombre
            });
        }

        private bool sc_estadoVisitaExists(int id)
        {
            return _context.estadoVisitas.Any(e => e.evi_id == id);
        }
    }
}
