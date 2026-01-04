namespace MonitoramentoEscolarAPI.Models
{
    public class NotificacaoModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioRemetenteId { get; set; }
        public Guid UsuarioDestinoId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensagem { get; set; } = null!;
        public bool Lida { get; set; } = false;
        public DateTime DataEnvio { get; set; } = DateTime.Now;

        public UsuarioModel Usuario { get; set; } = null!;

    }
}
