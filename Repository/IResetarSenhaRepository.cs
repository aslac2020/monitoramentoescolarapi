using MonitoramentoEscolarAPI.DTOs;

namespace MonitoramentoEscolarAPI.Repository
{
    public interface IResetarSenhaRepository
    {
        Task<(bool Sucess, string Message, string token)> SolicitarSenhaNova(SolicitarSenhaRequest request);
        Task<(bool Sucess, string Message)> ResetarSenha(ResetarSenhaRequest request);
    }
}
