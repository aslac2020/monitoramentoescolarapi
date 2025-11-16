namespace MonitoramentoEscolarAPI.Models
{
    public class RotaAlunoModel
    {
        public Guid RotaId { get; set;}
        public RotaModel Rota { get;set;}

        public Guid AlunoId { get; set;}
        public AlunoModel Aluno { get; set;}
        public int Ordem { get; set;}
    }
}
