using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoEscolarAPI.Models
{
    public static class Role
     {
        public const string RESPONSAVEL = "RESP";
        public const string MOTORISTA = "MOT";
        public const string GESTOR = "GEST";
    }

    public class UsuarioModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("senha")]
        public string Senha { get; set; } = null!;

        [Column("ativo")]
        public bool? Ativo { get; set; } = true;

        [Column("id_tipo_usuario")]
        public int IdTipoUsuario { get; set; }

        public TipoUsuarioModel TipoUsuario { get; set; }



    }




}
