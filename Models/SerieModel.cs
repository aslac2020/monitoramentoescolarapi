using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEscolarAPI.Models
{
    public class SerieModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
