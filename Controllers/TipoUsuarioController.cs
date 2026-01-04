using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository) => _tipoUsuarioRepository = tipoUsuarioRepository;

        [HttpGet("listar")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TipoUsuarioResponse>>> BuscarTodosUsuarios()
        {
            var usuarios = await _tipoUsuarioRepository.ListarTipoUsuario();
            return Ok(usuarios);
        }


    }
}
