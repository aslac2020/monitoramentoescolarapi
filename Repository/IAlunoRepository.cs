using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;

namespace MonitoramentoEscolarAPI.Repository
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<AlunoModel>> ListarTodosAlunos();
        Task<AlunoModel?> BuscarAlunoPorId(Guid id);
        Task<AlunoResponseDto> CadastrarAluno(AlunoCreateDto request);
        Task<(bool Sucess, string Message)> AtualizarAluno(Guid id, AlunoUpdateDto request);
        Task<(bool Sucess, string Message)> DeletarAluno(Guid id);

    }
}
