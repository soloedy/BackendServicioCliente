using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsServicioCliente.Datos;
using WsServicioCliente.Entidades.Puestos;

namespace WsServicioCliente.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class supervisorController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public supervisorController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/supervisor/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<supervisorViewModel>> listar()
        {
            var supervisor = await _context.supervisores.ToListAsync();

            return supervisor.Select(sup => new supervisorViewModel
            {
                sup_id =  sup.sup_id,
                sup_nombre = sup.sup_nombre,
                sup_correo = sup.sup_correo,
                sup_telefono = sup.sup_telefono,
                sup_identificacion = sup.sup_identificacion,
                sup_fechaNacimiento = sup.sup_fechaNacimiento,
                sup_fechaIngreso = sup.sup_fechaIngreso,
                sup_estado = sup.sup_estado
            });
        }

        // GET: api/supervisor/mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute]int id)
        {
            var sup = await _context.supervisores.SingleOrDefaultAsync(sup => sup.sup_id == id);

            if (sup == null)
            {
                return NotFound();
            }

            return Ok(new supervisorViewModel
            {
                sup_id = sup.sup_id,
                sup_nombre = sup.sup_nombre,
                sup_correo = sup.sup_correo,
                sup_telefono = sup.sup_telefono,
                sup_identificacion = sup.sup_identificacion,
                sup_fechaNacimiento = sup.sup_fechaNacimiento,
                sup_fechaIngreso = sup.sup_fechaIngreso,
                sup_estado = sup.sup_estado
            });
        }

        // PUT: api/supervisor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActulizarSupervisorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.sup_id < 0)
            {
                return BadRequest();
            }

            var supervisor = await _context.supervisores.FirstOrDefaultAsync(sup => sup.sup_id == model.sup_id);

            if (supervisor == null)
            {
                return NotFound();
            }

                supervisor.sup_id = model.sup_id;
                supervisor.sup_nombre = model.sup_nombre;
                supervisor.sup_correo = model.sup_correo;
                supervisor.sup_identificacion = model.sup_identificacion;
                supervisor.sup_fechaNacimiento = model.sup_fechaNacimiento;
            supervisor.sup_telefono = model.sup_telefono;
                supervisor.sup_fechaIngreso = model.sup_fechaIngreso;
            supervisor.sup_estado = true;

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

        // POST: api/cliente/crear
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<sc_supervisor>> Crear([FromBody] CrearSupervisorViewModel model)
        {
            int maxId = 0;
            var SupervisorConsulta = await _context.supervisores.ToListAsync();

            if (SupervisorConsulta.Count > 0)
            {
                maxId = _context.supervisores.Max(sup => sup.sup_id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_supervisor supervisor = new sc_supervisor
            {
                sup_id = maxId + 1,
                sup_nombre = model.sup_nombre,
                sup_correo = model.sup_correo,
                sup_identificacion = model.sup_identificacion,
                sup_telefono = model.sup_telefono,
                sup_fechaNacimiento = model.sup_fechaNacimiento,
                sup_fechaIngreso = model.sup_fechaIngreso,
                sup_estado = true,
        };

            _context.supervisores.Add(supervisor);
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
        // PUT: api/supervisor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var supervisor = await _context.supervisores.FirstOrDefaultAsync(sup => sup.sup_id == id);

            if (supervisor == null)
            {
                return NotFound();
            }
            supervisor.sup_estado = false;

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
        // PUT: api/supervisor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var supervisor = await _context.supervisores.FirstOrDefaultAsync(sup => sup.sup_id == id);

            if (supervisor == null)
            {
                return NotFound();
            }
            supervisor.sup_estado = true;

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

        private bool sc_supervisorExists(int id)
        {
            return _context.supervisores.Any(e => e.sup_id == id);
        }
    }
}
