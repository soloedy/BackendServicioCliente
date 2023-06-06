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
    public class visitaController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public visitaController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/visita/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<visitaViewModel>> listar()
        {
            var visita = await _context.visitas.
                Include(evi => evi.estadoVisita).
                Include(clie => clie.cliente).
                Include(dir => dir.direccion).ToListAsync();

            return visita.Select(visita => new visitaViewModel
            {
                 vis_id = visita.vis_id,
                 vis_fechaProgramada = visita.vis_fechaProgramada,
                 vis_fechaVisita = visita.vis_fechaVisita,
                 vis_horaIngreso = visita.vis_horaIngreso,
                 vis_horaSalida = visita.vis_horaSalida,
                 evi_id = visita.evi_id, 
                 vis_Descripcion = visita.vis_descripcion,
                 vis_resolucion = visita.vis_resolucion,
                 estadoVisita = visita.estadoVisita,
                 clie_id = visita.clie_id,    
                cliente = visita.cliente,
                dir_id = visita.dir_id, 
                direccion = visita.direccion
            });
        }
        // GET: api/visita/mostrarEstado/id
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<visitaViewModel>> mostrarEstado([FromRoute] int id)
        {
            var visita = await _context.visitas.
                Include(evi => evi.estadoVisita).
                Include(clie => clie.cliente).
                Where(vis => vis.evi_id == id).
                Include(dir => dir.direccion).ToListAsync();

            return visita.Select(visita => new visitaViewModel
            {
                vis_id = visita.vis_id,
                vis_fechaProgramada = visita.vis_fechaProgramada,
                vis_fechaVisita = visita.vis_fechaVisita,
                vis_horaIngreso = visita.vis_horaIngreso,
                vis_horaSalida = visita.vis_horaSalida,
                evi_id = visita.evi_id,
                vis_Descripcion = visita.vis_descripcion,
                estadoVisita = visita.estadoVisita,
                clie_id = visita.clie_id,
                cliente = visita.cliente,
                dir_id = visita.dir_id,
                direccion = visita.direccion
            });
        }

        // GET: api/visita/mostrarEstado/id
        [HttpGet("[action]")]
        public async Task<IEnumerable<visitaViewModel>> visitaAsignada()
        {
            var visita = await _context.visitas.
                Include(evi => evi.estadoVisita).
                Include(clie => clie.cliente).
                Where(vis => vis.evi_id == 2 || vis.evi_id == 3).
                Include(dir => dir.direccion).ToListAsync();

            return visita.Select(visita => new visitaViewModel
            {
                vis_id = visita.vis_id,
                vis_fechaProgramada = visita.vis_fechaProgramada,
                vis_fechaVisita = visita.vis_fechaVisita,
                vis_horaIngreso = visita.vis_horaIngreso,
                vis_horaSalida = visita.vis_horaSalida,
                evi_id = visita.evi_id,
                vis_Descripcion = visita.vis_descripcion,
                estadoVisita = visita.estadoVisita,
                clie_id = visita.clie_id,
                cliente = visita.cliente,
                dir_id = visita.dir_id,
                direccion = visita.direccion
            });
        }

        // GET: api/visita/mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute] int id)
        {
            var visita = await _context.visitas.
                Include(evi => evi.estadoVisita).
                Include(clie => clie.cliente).
                Include(dir => dir.direccion).SingleOrDefaultAsync(vis => vis.vis_id == id);

            if (visita == null)
            {
                return NotFound();
            }

            return Ok(new visitaViewModel
            {
                vis_id = visita.vis_id,
                vis_fechaProgramada = visita.vis_fechaProgramada,
                vis_fechaVisita = visita.vis_fechaVisita,
                vis_horaIngreso = visita.vis_horaIngreso,
                vis_horaSalida = visita.vis_horaSalida,
                vis_Descripcion = visita.vis_descripcion,
                evi_id = visita.evi_id,
                estadoVisita = visita.estadoVisita,
                clie_id = visita.clie_id,
                cliente = visita.cliente,
                dir_id = visita.dir_id,
                direccion = visita.direccion
            });
        }

        // PUT: api/visita/actualizarEstado
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> actualizarEstado([FromBody] ActualizarEstadoVisitaViewModel model)
        {
            try
            {
                    if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (model.vis_id < 0)
                {
                    return BadRequest();
                }

                var visitas = await _context.visitas.FirstOrDefaultAsync(vis => vis.vis_id == model.vis_id);

                if (visitas == null)
                {
                    return NotFound();
                }

                visitas.evi_id = model.evi_id;


                    await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }        
        // PUT: api/visita/actualizarEstado
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> actualizarProceso([FromRoute] int id)
        {
            try
            {
                var visitas = await _context.visitas.FirstOrDefaultAsync(vis => vis.vis_id == id);

                if (visitas == null)
                {
                    return NotFound();
                }

                visitas.evi_id = 3;
                visitas.vis_fechaVisita = DateTime.Today.Date;
                visitas.vis_horaIngreso = DateTime.Now.TimeOfDay;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
        // PUT: api/visita/actualizarEstado
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> VisitaFinalizada([FromBody] FinalizarEstadoVisitaViewModel model)
        {
            try
            {
                var visitas = await _context.visitas.FirstOrDefaultAsync(vis => vis.vis_id == model.vis_id);

                if (visitas == null)
                {
                    return NotFound();
                }

                visitas.evi_id = 4;
                visitas.vis_horaSalida = DateTime.Now.TimeOfDay;
                visitas.vis_resolucion = model.vis_resolucion;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }

        // PUT: api/visita/actualizarDireccion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> actualizarDireccion([FromBody] ActualizarDireccionVisitaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.vis_id < 0)
            {
                return BadRequest();
            }

            var visitas = await _context.visitas.FirstOrDefaultAsync(vis => vis.vis_id == model.vis_id);

            if (visitas == null)
            {
                return NotFound();
            }

            visitas.dir_id = model.dir_id;

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
        // POST: api/tecnico
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<sc_visita>> crear([FromBody] CrearVisitaViewModel model)
        {
            try
            {
                int maxId = 0;
                var visitaConsulta = await _context.visitas.ToListAsync();


                if (visitaConsulta.Count > 0)
                {
                    maxId = _context.visitas.Max(vis => vis.vis_id);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                sc_visita visita = new sc_visita
                {
                    vis_id = maxId + 1,
                    vis_fechaProgramada = model.vis_fechaProgramada,
                    evi_id = model.evi_id,
                    clie_id = model.clie_id,
                    dir_id = model.dir_id,
                    vis_descripcion = model.vis_descripcion
                };

                _context.visitas.Add(visita);

                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        private bool sc_visitaExists(int id)
        {
            return _context.visitas.Any(e => e.vis_id == id);
        }
    }
}
