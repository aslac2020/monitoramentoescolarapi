using MonitoramentoEscolarAPI.DTOs;

namespace MonitoramentoEscolarAPI.Repository
{
    public interface IAuthRepository
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);

    }
}
