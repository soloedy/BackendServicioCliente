using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsServicioCliente.Datos;
using WsServicioCliente.Entidades.Cliente;
using WsServicioCliente.Web.Models.Cliente;

namespace WsServicioCliente.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionesController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public TipoIdentificacionesController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificaciones/listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<sc_TipoIdentificacionViewModel>> listar()
        {
            var tipoIdentificacion = await _context.TipoIdenficaciones.ToListAsync();

            return tipoIdentificacion.Select(ti => new sc_TipoIdentificacionViewModel
            {
                ide_id = ti.ide_id,
                ide_descripcion = ti.ide_descripcion,
                ide_estado = ti.ide_estado
            });
        }

        // GET: api/TipoIdentificaciones/mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute]int id)
        {
            var tipoIdentificacion = await _context.TipoIdenficaciones.FindAsync(id);

            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            return Ok(new sc_TipoIdentificacionViewModel
            {
                ide_id = tipoIdentificacion.ide_id,
                ide_descripcion = tipoIdentificacion.ide_descripcion,
                ide_estado = tipoIdentificacion.ide_estado
            });
        }

        // PUT: api/TipoIdentificaciones/actualizar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarTipoIdentificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.ide_id < 0)
            {
                return BadRequest();
            }

            var tipoIdentificacion = await _context.TipoIdenficaciones.FirstOrDefaultAsync(ti => ti.ide_id == model.ide_id);

            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            tipoIdentificacion.ide_id = model.ide_id;
            tipoIdentificacion.ide_descripcion = model.ide_descripcion;
            tipoIdentificacion.ide_estado = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest();
            }
            return Ok();
        }

        // POST: api/TipoIdentificaciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearTipoIdentificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_TipoIdentificacion tipoIdentificacion = new sc_TipoIdentificacion
            {
                ide_id = model.ide_id,
                ide_descripcion = model.ide_descripcion,
                ide_estado = true
            };

            _context.TipoIdenficaciones.Add(tipoIdentificacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/TipoIdentificaciones/desactivar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> desactivar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var tipoIdentificacion = await _context.TipoIdenficaciones.FirstOrDefaultAsync(ti => ti.ide_id == id);

            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            tipoIdentificacion.ide_estado = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest();
            }
            return Ok();
        }

        // PUT: api/TipoIdentificaciones/desactivar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> activar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var tipoIdentificacion = await _context.TipoIdenficaciones.FirstOrDefaultAsync(ti => ti.ide_id == id);

            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            tipoIdentificacion.ide_estado = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest();
            }
            return Ok();
        }

        private bool sc_TipoIdentificacionExists(int id)
        {
            return _context.TipoIdenficaciones.Any(e => e.ide_id == id);
        }
    }
}
