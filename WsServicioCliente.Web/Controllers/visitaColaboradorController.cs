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
    public class visitaColaboradorController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public visitaColaboradorController(DbContextWsServicioClientes context)
        {
            _context = context;
        }
        // GET: api/visita/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<visitaColaboradorViewModel>> listar()
        {
            var visita = await _context.visitaColaboradores.
                Include(vis => vis.visita).
                Include(vis => vis.visita.cliente).
                Include(cli => cli.visita.direccion).
                Include(evi => evi.visita.estadoVisita).
                Include(sup => sup.supervisor).
                Include(tec => tec.tecnico).
                Where(evi => evi.visita.evi_id == 2 || evi.visita.evi_id == 3).
                ToListAsync();

            return visita.Select(visita => new visitaColaboradorViewModel
            {
                vis_id = visita.vis_id,
                visita = visita.visita,
                sup_id = visita.sup_id,
                supervisor = visita.supervisor,
                tec_id = visita.tec_id,
                tecnico = visita.tecnico
            });
        }
        // GET: api/visita/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<visitaColaboradorViewModel>> listarFinalizadas()
        {
            var visita = await _context.visitaColaboradores.
                Include(vis => vis.visita).
                Include(vis => vis.visita.cliente).
                Include(cli => cli.visita.direccion).
                Include(evi => evi.visita.estadoVisita).
                Include(sup => sup.supervisor).
                Include(tec => tec.tecnico).
                Where(evi => evi.visita.evi_id == 4).
                OrderByDescending(vis => vis.vis_id).
                ToListAsync();

            return visita.Select(visita => new visitaColaboradorViewModel
            {
                vis_id = visita.vis_id,
                visita = visita.visita,
                sup_id = visita.sup_id,
                supervisor = visita.supervisor,
                tec_id = visita.tec_id,
                tecnico = visita.tecnico
            });
        }
        // GET: api/visita/listar
        [HttpGet("[action]/{id}/{idEstado}")]
        public async Task<IEnumerable<visitaColaboradorViewModel>> visitasClientes([FromRoute] int id, int idEstado)
        {
            var visita = await _context.visitaColaboradores.
                Include(vis => vis.visita).
                Include(vis => vis.visita.cliente).
                Include(cli => cli.visita.direccion).
                Include(evi => evi.visita.estadoVisita).
                Include(sup => sup.supervisor).
                Include(tec => tec.tecnico).
                Where(evi => evi.visita.evi_id == idEstado).
                Where(evi => evi.visita.cliente.clie_id == id).
                OrderByDescending(vis => vis.vis_id).
                ToListAsync();

            return visita.Select(visita => new visitaColaboradorViewModel
            {
                vis_id = visita.vis_id,
                visita = visita.visita,
                sup_id = visita.sup_id,
                supervisor = visita.supervisor,
                tec_id = visita.tec_id,
                tecnico = visita.tecnico
            });
        }
        // GET: api/visita/listar
        [HttpGet("[action]/{id}/{idEstado}")]
        public async Task<IEnumerable<visitaColaboradorViewModel>> visitasTecnicos([FromRoute] int id, int idEstado)
        {
            var visita = await _context.visitaColaboradores.
                Include(vis => vis.visita).
                Include(vis => vis.visita.cliente).
                Include(cli => cli.visita.direccion).
                Include(evi => evi.visita.estadoVisita).
                Include(sup => sup.supervisor).
                Include(tec => tec.tecnico).
                Where(evi => evi.visita.evi_id == idEstado).
                Where(evi => evi.tec_id == id).
                OrderByDescending(vis => vis.vis_id).
                ToListAsync();

            return visita.Select(visita => new visitaColaboradorViewModel
            {
                vis_id = visita.vis_id,
                visita = visita.visita,
                sup_id = visita.sup_id,
                supervisor = visita.supervisor,
                tec_id = visita.tec_id,
                tecnico = visita.tecnico
            });
        }
        // GET: api/visita/listar
        [HttpGet("[action]/{id}/{idEstado}")]
        public async Task<IEnumerable<visitaColaboradorViewModel>> visitasSupervisores([FromRoute] int id, int idEstado)
        {
            var visita = await _context.visitaColaboradores.
                Include(vis => vis.visita).
                Include(vis => vis.visita.cliente).
                Include(cli => cli.visita.direccion).
                Include(evi => evi.visita.estadoVisita).
                Include(sup => sup.supervisor).
                Include(tec => tec.tecnico).
                Where(evi => evi.visita.evi_id == idEstado).
                Where(evi => evi.sup_id == id).
                OrderByDescending(vis => vis.vis_id).
                ToListAsync();

            return visita.Select(visita => new visitaColaboradorViewModel
            {
                vis_id = visita.vis_id,
                visita = visita.visita,
                sup_id = visita.sup_id,
                supervisor = visita.supervisor,
                tec_id = visita.tec_id,
                tecnico = visita.tecnico
            });
        }
        // GET: api/visitaColaborador
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute] int id)
        {
            var colaborador = await _context.visitaColaboradores.
                Include(sup => sup.supervisor).
                Include(tec => tec.tecnico).SingleOrDefaultAsync(sc => sc.vis_id == id);

            if (colaborador == null)
            {
                return NotFound();
            }
            return Ok(new visitaColaboradorViewModel
            {
                vis_id = colaborador.vis_id,
                sup_id = colaborador.sup_id,
                supervisor = colaborador.supervisor,
                tec_id = colaborador.tec_id,
                tecnico = colaborador.tecnico
            });

        }

        // PUT: api/visitaColaborador/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> actualizar([FromBody] CrearVisitaColaboradorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.tec_id < 0)
            {
                return BadRequest();
            }

            var visita = await _context.visitaColaboradores.FirstOrDefaultAsync(vc => vc.vis_id == model.vis_id);

            if (visita == null)
            {
                return NotFound();
            }

            visita.sup_id = model.sup_id;
            visita.tec_id = model.tec_id;

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

        // POST: api/visitaColaborador
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<sc_visitaColaborador>> crear([FromBody] CrearVisitaColaboradorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                sc_visitaColaborador visita = new sc_visitaColaborador
                {
                    vis_id = model.vis_id,
                    sup_id = model.sup_id,
                    tec_id = model.tec_id
                };
                _context.visitaColaboradores.Add(visita);

                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        private bool sc_visitaColaboradorExists(int id)
        {
            return _context.visitaColaboradores.Any(e => e.vis_id == id);
        }
    }
}
