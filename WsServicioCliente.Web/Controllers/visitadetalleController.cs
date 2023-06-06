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
    public class visitadetalleController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public visitadetalleController(DbContextWsServicioClientes context)
        {
            _context = context;
        }
        // GET: api/visita/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<visitaDetalleViewModel>> listar()
        {
            var visita = await _context.visitadetalles.
                Include(vis => vis.visita).
                Include(vis => vis.visita.cliente).
                Include(cli => cli.visita.direccion).
                Include(evi => evi.visita.estadoVisita).
                Where(evi => evi.visita.evi_id == 3).
                ToListAsync();

            return visita.Select(visita => new visitaDetalleViewModel
            {
                vis_id = visita.vis_id,
                visita = visita.visita,
            });
        }
        // GET: api/visitadetalle/mostrar/id
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute] int id)
        {
            var visitaDetalle = await _context.visitadetalles.SingleOrDefaultAsync(vis => vis.vis_id == id);

            if (visitaDetalle == null)
            {
                return NotFound();
            }
            return Ok(new visitaDetalleViewModel
            {
                vis_id = visitaDetalle.vis_id,
                visd_correlativo = visitaDetalle.visd_correlativo,
                visd_observaciones = visitaDetalle.visd_observaciones
            });
        }

        // PUT: api/visitadetalle/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> actualizar([FromBody] actualizarVisitaDetalleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(model.vis_id < 0)
            {
                return BadRequest();
            }
            var detalle = await _context.visitadetalles.
                FirstOrDefaultAsync(det => det.vis_id == model.vis_id || det.visd_correlativo == model.visd_correlativo);

            if(detalle == null)
            {
                return NotFound();
            }

            detalle.visd_observaciones = model.visd_observaciones;

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

        // POST: api/visitadetalle/crear
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult> crear([FromBody] visitaDetalleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_visitadetalle detalle = new sc_visitadetalle
            {
                vis_id = model.vis_id,
                visd_correlativo = model.visd_correlativo,
                visd_observaciones = model.visd_observaciones
            };

            _context.visitadetalles.Add(detalle);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/visitadetalle/5
        [HttpDelete("[action]")]
        public async Task<ActionResult<sc_visitadetalle>> eliminar([FromBody] eliminarVisitaDetalleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.vis_id < 0)
            {
                return BadRequest();
            }
            var detalle = await _context.visitadetalles.
                FirstOrDefaultAsync(det => det.vis_id == model.vis_id || det.visd_correlativo == model.visd_correlativo);

            _context.visitadetalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool sc_visitadetalleExists(int id)
        {
            return _context.visitadetalles.Any(e => e.vis_id == id);
        }
    }
}
