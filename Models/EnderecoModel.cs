using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEscolarAPI.Models
{

    public class EnderecoModel
    {
       [Key]
        public Guid Id { get; set; }

        public string Rua { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;

        public double? Lat { get; set; }
        public double? Lon { get; set; }


    }




}
