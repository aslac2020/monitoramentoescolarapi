using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoEscolarAPI.Models
{
    public static class Role
     {
        public const string RESPONSAVEL = "RESPONSAVEL";
        public const string MOTORISTA = "MOTORISTA";
        public const string GESTOR = "GESTOR";
     }

    public class UsuarioModel
    {
       [Key]
       [Column("id")]
       public Guid Id { get; set;}

       [Column("nome")]
       public string Nome { get; set;  } = null!;
        
       [Column("email")]
       public string Email { get; set;  } = null!;

       [Column("senha")]
       public string Senha { get; set;  } = null!;

       [Column("tipo")]
       public string Tipo { get; set;}
            
        
    }
}
