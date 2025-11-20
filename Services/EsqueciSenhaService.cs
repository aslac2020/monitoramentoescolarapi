using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Services
{
    public class EsqueciSenhaService : IEsqueciSenhaRepository
    {
        public readonly ApplicationDbContext _db;
        public readonly IConfiguration _configuration;
        private readonly EnviarEmailService _enviarEmailService;

        public EsqueciSenhaService(ApplicationDbContext db, 
            IConfiguration configuration, EnviarEmailService enviarEmailService)
        {
            _db = db;
            _configuration = configuration;
            _enviarEmailService = enviarEmailService;
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

           var hash = BCrypt.Net.BCrypt.HashPassword(request.NovaSenha);

            // troca a senha
            usuario.Senha = hash;

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
             var token = new Random().Next(100000, 999999).ToString();

            var reset = new ResetarSenhaModel
            {
                UsuarioId = usuario.Id,
                Token = token,
                ExpiraEm = DateTime.UtcNow.AddMinutes(15)
            };

            _db.ResetarSenhas.Add(reset);
            await _db.SaveChangesAsync();
            await EnviarEmailComToken(token, request.Email);


            return (true, "Token criado com sucesso.", token);
        }

        private async Task<(bool Sucess, string Message)> EnviarEmailComToken(string tokenRecebido, string email)
        {


            var token = tokenRecebido;


            string html = $@"
            <p>Seu código para redefinir a senha é:</p>
            <h2>{token}</h2>
            <p>O código expira em 15 minutos.</p>
            ";


            await _enviarEmailService.EnviarEmailAsync(
                email,
                "Recuperação de Senha - Monitoramento Escolar",
                html
            );

            return (true, "Um código foi enviado para seu e-mail.");
        }
    }
}
