using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Repository;
using MonitoramentoEscolarAPI.Services;

namespace MonitoramentoEscolarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticaoUsuarioController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEsqueciSenhaRepository _esqueciSenhaRepository;
        private readonly EnviarEmailService _enviarEmailService;


        public AutenticaoUsuarioController(IAuthRepository authRepository, 
            EnviarEmailService enviarEmailService, IEsqueciSenhaRepository esqueciSenhaRepository)
        {
             _authRepository = authRepository;
            _enviarEmailService = enviarEmailService;
            _esqueciSenhaRepository = esqueciSenhaRepository;
        }
            

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var res = await _authRepository.LoginAsync(request);
            if (res == null) return Unauthorized(new { message = "Email ou senha inválidos." });
            return Ok(res);

        }


        [HttpPost("solicitar")]
        [AllowAnonymous]
        public async Task<IActionResult> SolicitarNovaSenha([FromBody] SolicitarSenhaRequest request)
        {
            var result = await _esqueciSenhaRepository.SolicitarSenhaNova(request);
            if (!result.Sucess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });

        }

        [HttpPost("resetar")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetarSenha([FromBody] ResetarSenhaRequest dto)
        {
            var result = await _esqueciSenhaRepository.ResetarSenha(dto);
            if (!result.Sucess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });

        }







    }
}
