using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoController(IAlunoRepository alunoRepository) => _alunoRepository = alunoRepository;

         /// <summary>
        /// Lista todos os alunos cadastrados.
        /// </summary>
        /// <returns>Lista de objetos AlunoModel.</returns>
        [HttpGet]
        [Authorize(Roles = $"{Role.GESTOR}, {Role.MOTORISTA}")]
        public async Task<ActionResult<IEnumerable<AlunoModel>>> ListarTodosAlunos()
        {
            var alunos = await _alunoRepository.ListarTodosAlunos();
            return Ok(alunos);
        }

         /// <summary>
        /// Lista aluno pelo seu cadastro Id.
        /// </summary>
        /// <returns>Lista de objetos AlunoModel.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> BuscarAlunoPorId(Guid id)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorId(id);
            if (aluno == null)
                return NotFound(new { message = "Aluno não encontrado." });

            return Ok(aluno);
        }

          /// <summary>
        /// Cadastrar um Aluno.
        /// </summary>
        /// <returns>Lista de objetos AlunoModel.</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarAluno([FromBody] AlunoCreateDto aluno)
        {
            var result = await _alunoRepository.CadastrarAluno(aluno);
            if (result == null)
                return BadRequest(new { message = "Aluno não cadastrado" });

            return Ok(result);
        }

        
          /// <summary>
        /// Atualizar um Aluno.
        /// </summary>
        /// <returns>Lista de objetos AlunoModel.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> AtualizarAluno(Guid id, [FromBody] AlunoUpdateDto aluno)
        {
            var result = await _alunoRepository.AtualizarAluno(id, aluno);
            if (!result.Sucess)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        
          /// <summary>
        /// Deletar um Aluno.
        /// </summary>
        /// <returns>Lista de objetos AlunoModel.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Role.GESTOR}, {Role.MOTORISTA}")]
        public async Task<IActionResult> DeletarAluno(Guid id)
        {
            var result = await _alunoRepository.DeletarAluno(id);
            if (!result.Sucess)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Message });
        }


    }

}
