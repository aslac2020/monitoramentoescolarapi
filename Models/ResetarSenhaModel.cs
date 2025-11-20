using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoEscolarAPI.Models
{
    public class ResetarSenhaModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("usuario_id")]
        public Guid UsuarioId { get; set; }

        [Column("token")]
        public string Token { get; set; } = null!;

        
        [Column("expira_em")]
        public DateTime ExpiraEm { get; set; }
    }
}
