using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Services
{
    public class TipoUsuarioService : ITipoUsuarioRepository
    {
        public readonly ApplicationDbContext _db;
        public readonly IConfiguration _configuration;

        public TipoUsuarioService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UsuarioModel>> ListarTodosUsuarios()
        {
            return await _db.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TipoUsuarioModel>> ListarTipoUsuario()
        {
            return await _db.TiposUsuarios.AsNoTracking().ToListAsync();
        }
    }
}
