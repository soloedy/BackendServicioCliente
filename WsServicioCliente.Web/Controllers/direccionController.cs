using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsServicioCliente.Datos;
using WsServicioCliente.Entidades.Cliente;

namespace WsServicioCliente.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class direccionController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public direccionController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/direccion/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<clienteDireccionViewModel>> listar()
        {
            var direccion = await _context.direcciones.Include(clie => clie.cliente).ToListAsync();

            return direccion.Select(direccion => new clienteDireccionViewModel
            {
                clie_id = direccion.clie_id,
                cliente = direccion.cliente,
                dir_descripcion = direccion.dir_descripcion,
                dir_direccion = direccion.dir_direccion,
                dir_latitud = direccion.dir_latitud,
                dir_longitud = direccion.dir_longitud
            });
        }

        // GET: api/direccion/mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute]int id)
        {
            try
            {
                var direccion = await _context.direcciones.Include(clie => clie.cliente).SingleOrDefaultAsync(dir => dir.clie_id == id);

                if (direccion == null)
                {
                    return NotFound();
                }

                return Ok(new clienteDireccionViewModel
                {
                    dir_id = direccion.dir_id,
                    clie_id = direccion.clie_id,
                    cliente = direccion.cliente,
                    dir_descripcion = direccion.dir_descripcion,
                    dir_direccion = direccion.dir_direccion,
                    dir_latitud = direccion.dir_latitud,
                    dir_longitud = direccion.dir_longitud
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/direccion/crear
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<sc_cliente>> crear([FromBody] CrearClienteDireccionViewModel model)
        {
            int maxId = 0;
            var direccion = await _context.direcciones.ToListAsync();

            if (direccion.Count > 0)
            {
                maxId = _context.direcciones.Max(vis => vis.dir_id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_clientedireccion clienteDireccion = new sc_clientedireccion
            {
                dir_id = maxId + 1,
                clie_id = model.clie_id,
                dir_descripcion = model.dir_descripcion,
                dir_direccion = model.dir_direccion,
                dir_latitud = model.dir_latitud,
                dir_longitud = model.dir_longitud
            };

            _context.direcciones.Add(clienteDireccion);
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

        private bool sc_clientedireccionExists(int id)
        {
            return _context.direcciones.Any(e => e.clie_id == id);
        }
    }
}
