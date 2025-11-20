namespace MonitoramentoEscolarAPI.DTOs
{
    public record  EnviarEmailRequest(string Para, string Assunto, string HTML);
    public record ResetarSenhaRequest(string Token, string NovaSenha);
    public record SolicitarSenhaRequest(string Email);
    public record LoginRequest(string nome, string email, string senha, string tipo );

}
