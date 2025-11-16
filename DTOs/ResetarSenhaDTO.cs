namespace MonitoramentoEscolarAPI.DTOs
{
    public record ResetarSenhaRequest(string Token, string NovaSenha);
    public record SolicitarSenhaRequest(string Email);
}
