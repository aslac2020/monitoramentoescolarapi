namespace MonitoramentoEscolarAPI.Models
{
    public class RotaModel
    {
        public Guid Id { get; set;}
        public string Nome { get; set;} = null!;
        public Guid MotoristaId { get; set;}
        public DateTime HoraPartida { get; set;}
        public ICollection<RotaAlunoModel> RotasAlunos { get; set;} = new List<RotaAlunoModel>();
    }
}
