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
    public class ClienteController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;

        public ClienteController(DbContextWsServicioClientes context)
        {
            _context = context;
        }

        // GET: api/Cliente/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<clienteViewModel>> listar()
        {
            var cliente = await _context.clientes.Include(ti => ti.tipoIdentificacion).ToListAsync();

            return cliente.Select(c => new clienteViewModel
            {
                clie_id = c.clie_id,
                clie_nombre = c.clie_nombre,
                clie_direccion = c.clie_direccion,
                clie_identificacion = c.clie_identificacion,
                clie_coordenada = c.clie_coordenada,
                clie_estado = c.clie_estado,
                clie_telefono = c.clie_telefono,
                clie_correo = c.clie_correo,
                ide_id = c.ide_id,
                tipoIdentificacion = c.tipoIdentificacion
            });
        }
        // GET: api/Cliente/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var cliente = await _context.clientes.Include(c => c.tipoIdentificacion).SingleOrDefaultAsync(clie => clie.clie_id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(new clienteViewModel
            {
                clie_id = cliente.clie_id,
                clie_nombre = cliente.clie_nombre,
                clie_direccion = cliente.clie_direccion,
                clie_identificacion = cliente.clie_identificacion,
                clie_coordenada = null,
                clie_estado = cliente.clie_estado,
                clie_correo = cliente.clie_correo,
                clie_telefono = cliente.clie_telefono,
                ide_id = cliente.ide_id,
                tipoIdentificacion = cliente.tipoIdentificacion
            });
        }
        // PUT: api/clientes/actualizar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.clie_id < 0)
            {
                return BadRequest();
            }

            var cliente = await _context.clientes.FirstOrDefaultAsync(ti => ti.clie_id == model.clie_id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.clie_id = model.clie_id;
            cliente.clie_nombre = model.clie_nombre;
            cliente.clie_direccion = model.clie_direccion;
            cliente.clie_identificacion = model.clie_identificacion;
            cliente.clie_coordenada = "";
            cliente.clie_correo = model.clie_correo;
            cliente.clie_estado = true;
            cliente.clie_telefono = model.clie_telefono;
            cliente.ide_id = model.ide_id;

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
        public async Task<ActionResult<sc_cliente>> Crear([FromBody] CrearClienteViewModel model)
        {
            int maxId = 0;
            var clienteConsulta = await _context.clientes.
                Include(c => c.tipoIdentificacion).ToListAsync();

            if (clienteConsulta.Count > 0)
            {
                maxId = _context.clientes.Max(c => c.clie_id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sc_cliente cliente = new sc_cliente
            {
                clie_id = (maxId + 1),
                clie_nombre = model.clie_nombre,
                clie_direccion = model.clie_direccion,
                clie_identificacion = model.clie_identificacion,
                clie_correo = model.clie_correo,
            clie_coordenada = "",
                clie_estado = true,
                clie_telefono = model.clie_telefono,
                ide_id = model.ide_id,
            };

            _context.clientes.Add(cliente);
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

        // PUT: api/cliente/desactivar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> desactivar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var cliente = await _context.clientes.FirstOrDefaultAsync(cli => cli.clie_id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.clie_estado = false;

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
        // PUT: api/cliente/activar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> activar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var cliente = await _context.clientes.FirstOrDefaultAsync(cli => cli.clie_id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.clie_estado = true;

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
    }
}
