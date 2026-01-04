using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEscolarAPI.Models
{

    public class UsuarioEnderecoModel
    {
        [Key]
        public Guid UsuarioId { get; set; }
        public Guid EnderecoId { get; set; }
        public UsuarioModel? Usuario { get; set; }
        public EnderecoModel? Endereco { get; set; }



    }




}
