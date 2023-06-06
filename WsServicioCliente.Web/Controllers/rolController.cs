using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsServicioCliente.Datos;
using WsServicioCliente.Entidades.Usuarios;

namespace WsServicioCliente.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class rolController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public rolController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/rol
        [HttpGet("[action]")]
        public async Task<IEnumerable<rolViewModel>> listar()
        {
            var rol = await _context.roles.ToListAsync();

            return rol.Select(rl => new rolViewModel
            {
                rol_id = rl.rol_id,
                rol_nombre = rl.rol_nombre,
                rol_descripcion = rl.rol_descripcion,
                rol_estado = rl.rol_estado
            });
        }

        // GET: api/rol/mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute]int id)
        {
            var rol = await _context.roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }
            return Ok(new rolViewModel
            {
                rol_id = rol.rol_id,
                rol_nombre = rol.rol_nombre,
                rol_descripcion = rol.rol_descripcion,
                rol_estado = rol.rol_estado
            });
        }

        // POST: api/rol/crear
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] crearRolViewModel model)
        {
            int correlativoRol = 0;
            var consultaCorrelativo = await _context.roles.ToListAsync();

            if (consultaCorrelativo.Count > 0)
            {
                correlativoRol = _context.roles.Max(rol => rol.rol_id);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_rol rol = new sc_rol
            {
                rol_id = (correlativoRol + 1),
                rol_nombre = model.rol_nombre,
                rol_descripcion = model.rol_descripcion,
                rol_estado = model.rol_estado
            };

            _context.roles.Add(rol);

            try
            {
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        private bool sc_rolExists(int id)
        {
            return _context.roles.Any(e => e.rol_id == id);
        }
    }
}
