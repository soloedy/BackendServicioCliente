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
    public class tecnicoController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public tecnicoController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/tecnico
        [HttpGet("[action]")]
        public async Task<IEnumerable<tecnicoViewModel>> listar()
        {
            var tec = await _context.tecnicos.Include(sup => sup.supervisor).ToListAsync();

            return tec.Select(tec => new tecnicoViewModel
            {
                tec_id = tec.tec_id,
                tec_nombre = tec.tec_nombre,
                tec_correo = tec.tec_correo,
                tec_identificacion = tec.tec_identificacion,
                tec_fechaNacimiento = tec.tec_fechaNacimiento,
                tec_fechaIngreso = tec.tec_fechaIngreso,
                tec_telefono = tec.tec_telefono,
                tec_estado = tec.tec_estado,
                sup_id = tec.sup_id,
                supervisor = tec.supervisor
            });
        }

        // GET: api/tecnico/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute] int id)
        {
            var tec = await _context.tecnicos.Include(sup => sup.supervisor).SingleOrDefaultAsync(tec => tec.tec_id == id);

            if (tec == null)
            {
                return NotFound();
            }

            return Ok(new tecnicoViewModel
            {
                tec_id = tec.tec_id,
                tec_nombre = tec.tec_nombre,
                tec_correo = tec.tec_correo,
                tec_identificacion = tec.tec_identificacion,
                tec_fechaNacimiento = tec.tec_fechaNacimiento,
                tec_telefono = tec.tec_telefono,
                tec_fechaIngreso = tec.tec_fechaIngreso,
                tec_estado = tec.tec_estado,
                sup_id = tec.sup_id,
                supervisor = tec.supervisor
            });
        }

        // PUT: api/tecnico/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> actualizar([FromBody] ActualizarTecnicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.tec_id < 0)
            {
                return BadRequest();
            }

            var tecnicos = await _context.tecnicos.FirstOrDefaultAsync(tec => tec.tec_id == model.tec_id);

            if (tecnicos == null)
            {
                return NotFound();
            }

            tecnicos.tec_id = model.tec_id;
            tecnicos.tec_nombre = model.tec_nombre;
            tecnicos.tec_correo = model.tec_correo;
            tecnicos.tec_telefono = model.tec_telefono;
            tecnicos.tec_identificacion = model.tec_identificacion;
            tecnicos.tec_fechaNacimiento = model.tec_fechaNacimiento;
            tecnicos.tec_fechaIngreso = model.tec_fechaIngreso;
            tecnicos.tec_estado = model.tec_estado;
            tecnicos.sup_id = model.sup_id;

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
        public async Task<ActionResult<sc_tecnico>> crear([FromBody] CrearTecnicoViewModel model)
        {
            int maxId = 0;
            var tecnicoConsulta = await _context.tecnicos.ToListAsync();

            if (tecnicoConsulta.Count > 0)
            {
                maxId = _context.tecnicos.Max(tec => tec.tec_id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_tecnico tecnico = new sc_tecnico
            {
                tec_id = maxId + 1,
                tec_nombre = model.tec_nombre,
                tec_correo = model.tec_correo,
                tec_telefono = model.tec_telefono,
                tec_identificacion = model.tec_identificacion,
                tec_fechaNacimiento = model.tec_fechaNacimiento,
                tec_fechaIngreso = model.tec_fechaIngreso,
                tec_estado = true,
                sup_id = model.sup_id
            };

            _context.tecnicos.Add(tecnico);
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

        // PUT: api/tecnico/desactivar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var tecnico = await _context.tecnicos.FirstOrDefaultAsync(tec => tec.tec_id == id);

            if (tecnico == null)
            {
                return NotFound();
            }
            tecnico.tec_estado = false;

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
        // PUT: api/tecnico/activar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> activar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var tecnico = await _context.tecnicos.FirstOrDefaultAsync(tec => tec.tec_id == id);

            if (tecnico == null)
            {
                return NotFound();
            }
            tecnico.tec_estado = true;

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

        private bool sc_tecnicoExists(int id)
        {
            return _context.tecnicos.Any(e => e.tec_id == id);
        }
    }
}
