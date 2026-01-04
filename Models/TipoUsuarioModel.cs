using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoEscolarAPI.Models
{
    public class TipoUsuarioModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codtipousuario")]
        public string CodTipoUsuario { get; set; }

        
        [Column("desctipousuario")]
        public string DescricaoTipoUsuario { get; set; }
    }
}
