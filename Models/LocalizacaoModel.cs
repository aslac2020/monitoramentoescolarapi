namespace MonitoramentoEscolarAPI.Models
{
    public class LocalizacaoModel
    {
        public Guid Id { get; set; }
        public Guid MotoristaId { get; set; }
        public MotoristaModel? Motorista { get; set; }
        public double Latitude { get; set;}
        public double Longitude { get; set;}
        public DateTime DataHora { get; set;} = DateTime.Now;
    }
}
