using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WsServicioCliente.Datos;
using WsServicioCliente.Entidades.Usuarios;
using WsServicioCliente.Web.Models.Usuarios.Usuarios;

namespace WsServicioCliente.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {
        private readonly DbContextWsServicioClientes _context;
        private readonly IConfiguration _config;

        public usuarioController(DbContextWsServicioClientes context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/usuario/listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<usuarioViewController>> listar()
        {
            var usuario = await _context.usuarios.Include(rol => rol.rol).ToListAsync();

            return usuario.Select(usua => new usuarioViewController
            {
                usua_id = usua.usua_id,
                usua_nombre = usua.usua_nombre,
                usua_email = usua.usua_email,
                usua_estado = usua.usua_estado,
                usua_password_hash = usua.usua_password_hash,
                rol = usua.rol
            });
        }

        // GET: api/usuario/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> mostrar([FromRoute] int id)
        {
            var usua = await _context.usuarios.Include(rol => rol.rol).SingleOrDefaultAsync(usua => usua.usua_id == id);

            if (usua == null)
            {
                return NotFound();
            }

            return Ok(new usuarioViewController
            {
                usua_id = usua.usua_id,
                usua_nombre = usua.usua_nombre,
                usua_email = usua.usua_email,
                usua_estado = usua.usua_estado,
                usua_password_hash = usua.usua_password_hash,
                rol = usua.rol
            });

        }

        // PUT: api/usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarUsuarioViewController model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.usua_id < 0)
            {
                return BadRequest();
            }
            var usuario = await _context.usuarios.FirstOrDefaultAsync(usua => usua.usua_id == model.usua_id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.usua_id = model.usua_id;
            usuario.usua_nombre = model.usua_nombre;
            usuario.usua_email = model.usua_email.ToLower();
            usuario.rol_id = model.rol_id;
            if (model.actpassword)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.usua_password_hash = passwordHash;
                usuario.usua_password_salt = passwordSalt;
            }
            usuario.usua_estado = true;

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

        // POST: api/usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<sc_usuario>> Crear([FromBody] crearUsuarioViewController model)
        {
            // Se declara una variable para obtener el correlativo siguiente para el usuario
            int correlativoUsuario = 0;
            var consultaCorrelativo = await _context.usuarios.ToListAsync();

            if (consultaCorrelativo.Count > 0)
            {
                correlativoUsuario = _context.usuarios.Max(usua => usua.usua_id);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.usua_email.ToLower();

            if (await _context.usuarios.AnyAsync(u => u.usua_email == email))
            {
                return BadRequest("El email enviado para el usuario ya existe.");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            sc_usuario usuario = new sc_usuario
            {
                usua_id = (correlativoUsuario + 1),
                rol_id = model.rol_id,
                usua_nombre = model.usua_nombre,
                usua_email = model.usua_email.ToLower(),
                usua_estado = true,
                usua_password_hash = passwordHash,
                usua_password_salt = passwordSalt
            };

            _context.usuarios.Add(usuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // POST: api/usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> Desactivar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.usuarios.FirstOrDefaultAsync(usua => usua.usua_id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.usua_estado = false;

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
        // POST: api/usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> activar([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.usuarios.FirstOrDefaultAsync(usua => usua.usua_id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.usua_estado = true;

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

        // POST: api/usuario/login
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<IActionResult> login([FromBody] LoginViewModel model)
        {
            var email = model.email.ToLower();
            var usuario = await _context.usuarios.Where(u => u.usua_estado == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.usua_email == model.email);

            if (usuario == null)
            {
                return NotFound("El usuario no existe");
            }

            if (!VerificarPasswordHash(model.password,usuario.usua_password_hash, usuario.usua_password_salt))
            {
                return NotFound("El usuario no existe o las credenciales ingresadas son incorrectas.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.usua_id.ToString()),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role, usuario.rol.rol_nombre),
                new Claim("idUsuario", usuario.usua_id.ToString()),
                new Claim("rol", usuario.rol.rol_nombre),
                new Claim("nombre", usuario.usua_nombre)
            };

            return Ok(
                new { token = GenerarToken(claims) });
        }
        // Médoto para verificar password.
        private bool VerificarPasswordHash(string password,byte[] passwordHashAlmacenado, byte[] passwordSaltAlmacenado)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSaltAlmacenado))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        // Método para crear token.
        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt.Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Método para encriptar password. 
        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool sc_usuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.usua_id == id);
        }
    }
}
