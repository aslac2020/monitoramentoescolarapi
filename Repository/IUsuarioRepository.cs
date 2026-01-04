using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;

namespace MonitoramentoEscolarAPI.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioModel>> ListarTodosUsuarios();
        Task<UsuarioModel?> BuscarUsuarioPorId(Guid id);
        Task<UsuarioModel?> BuscarUsuarioPorEmail(string email);
        Task<(bool Sucess, string Message, UsuarioModel?)> CadastrarUsuario(UsuarioRequest request);
        Task<(bool sucess, string message)> DeletarUsuario(Guid id);
        Task<(bool Sucess, string Message)> AtualizarUsuario(Guid id, UsuarioUpdateRequest request);

    }
}
