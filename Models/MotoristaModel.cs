namespace MonitoramentoEscolarAPI.Models
{
    public class MotoristaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Placa { get; set; } = null!;
        public Guid UsuarioId { get; set; }
        public bool? Ativo { get; set; } = true;
        public Guid? EnderecoId { get; set; }
        public EnderecoModel? Endereco { get; set; }

    }
}
