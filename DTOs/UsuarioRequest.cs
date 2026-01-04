namespace MonitoramentoEscolarAPI.DTOs
{
    public record UsuarioRequest(string nome, string email, string senha,  int idTipoUsuario );
    public record UsuarioUpdateRequest(string nome, string email, int idTipoUsuario);
}
