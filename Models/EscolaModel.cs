using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEscolarAPI.Models
{
    public class EscolaModel
    {
      [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Guid? EnderecoId { get; set; }
        public EnderecoModel? Endereco { get; set; }
        public bool? Ativo { get; set; } = true;
    }
}
