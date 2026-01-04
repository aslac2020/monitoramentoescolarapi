using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _auth;

        public UsuarioController(IUsuarioRepository auth) => _auth = auth;

        // GET: api/usuario
        [HttpGet("listar")]
        [Authorize(Roles = $"{Role.GESTOR}")]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> BuscarTodosUsuarios()
        {
            var usuarios = await _auth.ListarTodosUsuarios();
            return Ok(usuarios);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> BuscarUsuarioPorId(Guid id)
        {
            var usuario = await _auth.BuscarUsuarioPorId(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        [HttpGet("buscarUsuario")]
        [Authorize]
        public async Task<IActionResult> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _auth.BuscarUsuarioPorEmail(email);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UsuarioRequest usuario)
        {
            var result = await _auth.CadastrarUsuario(usuario);
            if (!result.Sucess)
                return BadRequest(new { message = result.Message });

            return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = result.Item3!.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> AtualizarUsuario(Guid id, [FromBody] UsuarioUpdateRequest usuario)
        {
            var result = await _auth.AtualizarUsuario(id, usuario);
            if (!result.Sucess)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Role.GESTOR}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _auth.DeletarUsuario(id);
            if (!result.sucess)
                return NotFound(new { message = result.message });

            return Ok(new { message = result.message });
        }

    }
}
