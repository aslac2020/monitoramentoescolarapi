using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResetarSenhaController : ControllerBase
    {
        private readonly IResetarSenhaRepository _resetarSenhaRepository;

        public ResetarSenhaController(IResetarSenhaRepository resetarSenhaRepository) => _resetarSenhaRepository = resetarSenhaRepository;


        [HttpPost("solicitar")]
        [AllowAnonymous]
        public async Task<IActionResult> SolicitarNovaSenha([FromBody] SolicitarSenhaRequest request)
        {
            var result = await _resetarSenhaRepository.SolicitarSenhaNova(request);
            if (!result.Sucess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message, Token = result.token });

        }

        [HttpPost("resetar")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetarSenha([FromBody] ResetarSenhaRequest dto)
        {
            var result = await _resetarSenhaRepository.ResetarSenha(dto);
            if (!result.Sucess)
                return BadRequest(new { message = result.Message });

            return Ok(new {message = result.Message});

        }





    }
}
