using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEscolarAPI.Models
{
    public class StatusAlunoRotaModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;

    }
}
