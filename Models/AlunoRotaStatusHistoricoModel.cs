using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEscolarAPI.Models
{
    public class AlunoRotaStatusHistoricoModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid AlunoId { get; set; }
        public AlunoModel? Aluno { get; set; }

        public Guid RotaId { get; set; }
        public RotaModel? Rota { get; set; }

        public Guid StatusId { get; set; }
        public StatusAlunoRotaModel? Status { get; set; }

        public DateTime DataHora { get; set; } = DateTime.UtcNow;

    }
}
