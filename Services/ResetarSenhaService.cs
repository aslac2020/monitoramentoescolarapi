using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Services
{
    public class ResetarSenhaService : IResetarSenhaRepository
    {

        public readonly ApplicationDbContext _db;
        public readonly IConfiguration _configuration;

        public ResetarSenhaService(ApplicationDbContext db,  IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<(bool Sucess, string Message)> ResetarSenha(ResetarSenhaRequest request)
        {
              var reset = await _db.ResetarSenhas
                .FirstOrDefaultAsync(t => t.Token == request.Token);

            if (reset == null)
                return (false, "Token inválido.");

            if (reset.ExpiraEm < DateTime.UtcNow)
                return (false, "Token expirado.");

            var usuario = await _db.Usuarios.FindAsync(reset.UsuarioId);
            if (usuario == null)
                return (false, "Usuário não encontrado.");

            // troca a senha
            usuario.Senha = request.NovaSenha;

            // invalida o token
            _db.ResetarSenhas.Remove(reset);

            await _db.SaveChangesAsync();
            return (true, "Senha atualizada com sucesso.");
        }

        public async Task<(bool Sucess, string Message, string token)> SolicitarSenhaNova(SolicitarSenhaRequest request)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (usuario == null)
                return (false, "E-mail não encontrado.", "");

            // cria token
            var token = Guid.NewGuid().ToString("N");

            var reset = new ResetarSenhaModel
            {
                UsuarioId = usuario.Id,
                Token = token,
                ExpiraEm = DateTime.UtcNow.AddMinutes(15)
            };

            _db.ResetarSenhas.Add(reset);
            await _db.SaveChangesAsync();

            return (true, "Token criado com sucesso.", token);
          
        }
    }
}
