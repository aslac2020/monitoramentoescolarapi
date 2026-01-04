using MonitoramentoEscolarAPI.Models;

namespace MonitoramentoEscolarAPI.Repository
{
    public interface ITipoUsuarioRepository
    {
        Task<IEnumerable<TipoUsuarioModel>> ListarTipoUsuario();
       

    }
}
