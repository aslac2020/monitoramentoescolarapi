using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MonitoramentoEscolarAPI.Services
{
    public class AuthService : IAuthRepository
    {

        public readonly ApplicationDbContext _db;
        public readonly IConfiguration _configuration;


        public AuthService(ApplicationDbContext db,  IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }



        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var usuario = _db.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefault(u => u.Email == request.email);

            if (usuario == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(request.senha, usuario.Senha)) return null;

            var jwt = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.CodTipoUsuario),
            };


            var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresMinutes"])),
            signingCredentials: creds
        );


            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginResponse(tokenStr, usuario.Nome, usuario.Email);


        }


       
    }
}
