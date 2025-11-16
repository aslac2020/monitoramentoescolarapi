using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                Tipo = request.tipo.ToUpper()
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return (true, "Usuário cadastrado com sucesso.", usuario);
      }


  
        public async Task<(bool Sucess, string Message)> AtualizarUsuario(Guid id, UsuarioModel request)
        {

            var usuario = await _db.Usuarios.FindAsync(id);
            var hash = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            if (usuario == null)
             return (false, "Usuário não encontrado.");

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;
            usuario.Senha = hash;
            usuario.Tipo = request.Tipo;

            _db.Entry(usuario).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return (true, "Usuário atualizado com sucesso");



        }   

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == request.Email);
            if (usuario == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha)) return null;

            var jwt = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
            };


            var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresMinutes"])),
            signingCredentials: creds
        );


           string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
           return  new LoginResponse(tokenStr, usuario.Nome, usuario.Email, usuario.Tipo.ToString());


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
