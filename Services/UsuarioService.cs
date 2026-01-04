using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Services
{
    public class UsuarioService : IUsuarioRepository
    {
        public readonly ApplicationDbContext _db;
        public readonly IConfiguration _configuration;

        public UsuarioService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UsuarioModel>> ListarTodosUsuarios()
        {
            return await _db.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<UsuarioModel?> BuscarUsuarioPorId(Guid id)
        {
            return await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UsuarioModel?> BuscarUsuarioPorEmail(string email)
        {
             return await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<(bool Sucess, string Message, UsuarioModel?)> CadastrarUsuario(UsuarioRequest request)
        {
            bool emailExistente = await _db.Usuarios.AnyAsync(u => u.Email == request.email);
            if (emailExistente)
                return (false, "Email já cadastrado.", null);

            var hash = BCrypt.Net.BCrypt.HashPassword(request.senha);
            var usuario = new UsuarioModel
            {
                Id = Guid.NewGuid(),
                Nome = request.nome,
                Email = request.email,
                Senha = hash,
                IdTipoUsuario = request.idTipoUsuario
        
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return (true, "Usuário cadastrado com sucesso.", usuario);
        }

     

        public async Task<(bool Sucess, string Message)> AtualizarUsuario(Guid id, UsuarioUpdateRequest request)
        {

            var usuario = await _db.Usuarios.FindAsync(id);
            if (usuario == null)
             return (false, "Usuário não encontrado.");

            usuario.Nome = request.nome;
            usuario.Email = request.email;
            usuario.IdTipoUsuario = request.idTipoUsuario;

            _db.Entry(usuario).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return (true, "Usuário atualizado com sucesso");



        }   

         public async Task<(bool sucess, string message)> DeletarUsuario(Guid id)
         {
            var usuario = await _db.Usuarios.FindAsync(id);
            if(usuario == null) 
            return (false, "Ususario Não encontradao");

             _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();

            return (true, "Usuário removido com sucesso.");
         }

      
    }
}
